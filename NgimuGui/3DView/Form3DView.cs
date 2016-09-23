using System;
using System.Drawing;
using System.Windows.Forms;
using NgimuApi.Maths;
using NgimuGui.DialogsAndWindows;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using Rug.LiteGL;
using Rug.LiteGL.Effect;
using Rug.LiteGL.Meshes;
using Rug.LiteGL.Simple;
using TextRenderer = Rug.LiteGL.Text.TextRenderer;

namespace NgimuGui
{
    /// <summary>
    /// 3D view form class.
    /// </summary>
    public partial class Form3DView : BaseForm
    {
        public readonly string ID = "Form3DView";

        /// <summary>
        /// Quaternion describing sensor relative to Earth.
        /// </summary>
        public NgimuApi.Maths.Quaternion quaternion;

        private Mesh axisModel;
        private SharedEffects effects = new SharedEffects();
        private Mesh floorTileModel;
        private string floorTileModelPath = "~/3DView/Floor Tile.obj";
        private Timer formUpdateTimer;
        private GLControl glControl1;
        private string housingModelPath = "~/3DView/NGIMU Housing Solid Body.obj";
        private Mesh imuModel;
        private bool isInErrorState = false;
        private LightingArguments lightingArguments;
        private ModelLightInstance[] lights;
        private Font m_BaseFont = new Font("Lucida Console", 24);

        //Font m_BaseFont = new Font(FontFamily.GenericMonospace, 24, FontStyle.Regular, GraphicsUnit.Point); //  "Lucida Console", 24);
        private Font m_BaseFont2 = new Font("Arial", 24);

        private Point m_LastMouseLocation;
        private bool m_MousePressed;
        private Font m_ResizedFont = null;
        private float m_ViewPitch = 0;
        private float m_ViewYaw = 0;
        private SimpleTexturedMaterial material;
        private float modelScale = 1f;
        private string pcbModelPath = "~/3DView/NGIMU Board Solid Body.obj";
        private Random rand = new Random();
        private ModelSceneLight[] sceneLights;
        private TextRenderer textRenderer;
        private TextureQuad texturedQuad;
        private SimpleTexturedMaterial tileMaterial;
        private Texture2D tileTexture;
        private View3D view;

        private Brush redBrush = new SolidBrush(Colors.Red);
        private Brush greenBrush = new SolidBrush(Colors.Green);
        private Brush blueBrush = new SolidBrush(Colors.Blue);

        public NgimuApi.Maths.EulerAngles EulerAngles
        {
            set
            {
                quaternion = NgimuApi.Maths.Quaternion.Normalise(NgimuApi.Maths.Quaternion.FromEulerAngles(value));
            }
        }

        /// <summary>
        /// Quaternion describing sensor relative to Earth.
        /// </summary>
        public NgimuApi.Maths.Quaternion Quaternion
        {
            get
            {
                return quaternion;
            }

            set
            {
                quaternion = NgimuApi.Maths.Quaternion.Normalise(value);
            }
        }

        public NgimuApi.Maths.RotationMatrix RotationMatrix
        {
            set
            {
                quaternion = NgimuApi.Maths.Quaternion.Normalise(NgimuApi.Maths.Quaternion.FromRotationMatrix(value));
            }
        }

        public Form3DView()
        {
            InitializeComponent();

            LoadOpenGLControl();

            imuModel = LoadModel("PCB", this.imuHousingToolStripMenuItem.Checked ? housingModelPath : pcbModelPath, true);
            axisModel = LoadModel("Axis", "~/3DView/Arrow.obj", false);
            floorTileModel = LoadModel("Floor Tile", floorTileModelPath, false);

            material = new SimpleTexturedMaterial(false);
            effects.Effects.Add(material.Name, material);

            tileMaterial = new SimpleTexturedMaterial(true);
            effects.Effects.Add(tileMaterial.Name, tileMaterial);

            effects.Effects.Add(SimpleTexturedQuad.GetName(BoxMode.Color), new SimpleTexturedQuad(BoxMode.Color));
            effects.Effects.Add(SimpleTexturedQuad.GetName(BoxMode.Textured), new SimpleTexturedQuad(BoxMode.Textured));
            effects.Effects.Add(SimpleTexturedQuad.GetName(BoxMode.TexturedColor), new SimpleTexturedQuad(BoxMode.TexturedColor));

            textRenderer = new TextRenderer(10, 10);
            texturedQuad = new TextureQuad(effects);
            texturedQuad.Texture = textRenderer.Texture;

            view = new View3D(glControl1.Bounds, glControl1.Bounds.Width, glControl1.Bounds.Height, (float)Math.PI / 4f);

            int lightCount = 1;
            //lightBuffer = new UniformBuffer("Lights", new UniformBufferInfo(ModelLightInstance.Format, 1, OpenTK.Graphics.OpenGL.BufferUsageHint.DynamicDraw));

            lights = new ModelLightInstance[lightCount];

            sceneLights = new ModelSceneLight[lightCount];
            sceneLights[0] = new ModelSceneLight()
            {
                Center = new OpenTK.Vector3(30, 80, 60) * 8000f,
                Color = Color4.White,
                SpecularPower = 1.2f,
                Radius = float.PositiveInfinity,
                AttenuatuionTermA = 0f,
                Intensity = 0.8f,
                Directional = true,
            };

            lightingArguments = new LightingArguments();

            Quaternion = NgimuApi.Maths.Quaternion.Identity;

            formUpdateTimer = new Timer();
            formUpdateTimer.Interval = 20;
            formUpdateTimer.Tick += new EventHandler(formUpdateTimer_Tick);

            tileTexture = new BitmapTexture2D("Tile Texture", "~/3DView/Compass.png", Texture2DInfo.Bitmap32_LinearClampToEdgeMipAnisotropicFilteringX4(new Size(32, 32)));
        }

        private static OpenTK.Vector3 UnProject(ref Matrix4 projection, ref Matrix4 view, Size viewport, Vector2 mouse)
        {
            Vector4 vec;

            vec.X = mouse.X; //  2.0f * mouse.X / (float)viewport.Width - 1;
            vec.Y = mouse.Y; // -(2.0f * mouse.Y / (float)viewport.Height - 1);
            vec.Z = 0.99f;
            vec.W = 1.0f;

            Matrix4 viewInv = Matrix4.Invert(view);
            Matrix4 projInv = Matrix4.Invert(projection);

            Vector4.Transform(ref vec, ref projInv, out vec);
            Vector4.Transform(ref vec, ref viewInv, out vec);

            if (vec.W > float.Epsilon || vec.W < float.Epsilon)
            {
                vec.X /= vec.W;
                vec.Y /= vec.W;
                vec.Z /= vec.W;
            }

            return vec.Xyz;
        }

        private void DisposeOpenGLControl()
        {
            Controls.Remove(glControl1);

            glControl1.Dispose();

            glControl1.Paint -= GlControl1_Paint;
            glControl1.Resize -= GlControl1_SizeChanged;
            //glControl1.KeyDown += glControl1_KeyDown;
            //glControl1.KeyPress += glControl1_KeyPress;
            //glControl1.KeyUp += glControl1_KeyUp;
            //glControl1.MouseClick += glControl1_MouseClick;
            glControl1.MouseDoubleClick -= GlControl1_MouseDoubleClick;
            glControl1.MouseDown -= GlControl1_MouseDown;
            glControl1.MouseMove -= GlControl1_MouseMove;
            glControl1.MouseUp -= GlControl1_MouseUp;
            glControl1.MouseWheel -= GlControl1_MouseWheel;

            glControl1.Load += GlControl1_Load;
            glControl1.Disposed += GlControl1_Disposed;
        }

        private void DrawText(ref Matrix4 worldProjectionMatrix, ref OpenTK.Vector3 textLocation, string text, Brush brush)
        {
            Vector4 vector = new Vector4(textLocation, 1f);
            Vector4.Transform(ref vector, ref worldProjectionMatrix, out vector);

            if (vector.Z > 0)
            {
                OpenTK.Vector3 point = vector.Xyz / vector.W;

                SizeF textSize = textRenderer.Graphics.MeasureString(text, m_BaseFont, 10000, textRenderer.StringFormat_CenterCenter);

                textRenderer.DrawString(text, m_BaseFont, brush, ToScreenPoint(point.X, point.Y, textSize), true);
            }
        }

        /// <summary>
        /// Form closing event to minimise form instead of close.
        /// </summary>
        private void Form3DView_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing ||
                e.CloseReason == CloseReason.None)
            {
                //this.WindowState = FormWindowState.Minimized;
                Hide();

                e.Cancel = true;
            }
        }

        private void Form3DView_Load(object sender, EventArgs e)
        {
            if (Options.Windows[ID].Bounds != Rectangle.Empty)
            {
                this.DesktopBounds = Options.Windows[ID].Bounds;
            }

            WindowState = Options.Windows[ID].WindowState;
        }

        /// <summary>
        /// Form visible changed event to start/stop form update formUpdateTimer.
        /// </summary>
        private void Form3DView_VisibleChanged(object sender, EventArgs e)
        {
            Options.Windows[ID].IsOpen = this.Visible;

            if (this.Visible)
            {
                formUpdateTimer.Start();
            }
            else
            {
                formUpdateTimer.Stop();
            }
        }

        /// <summary>
        /// Timer tick event to refresh graphics.
        /// </summary>
        private void formUpdateTimer_Tick(object sender, EventArgs e)
        {
            //lock (this)
            {
                glControl1.Invalidate();
            }
        }

        private void GlControl1_Disposed(object sender, EventArgs e)
        {
            try
            {
                effects.UnloadResources();

                imuModel.UnloadResources();
                axisModel.UnloadResources();
                floorTileModel.UnloadResources();
                tileTexture.UnloadResources();

                textRenderer.UnloadResources();
                texturedQuad.UnloadResources();

                //lightBuffer.UnloadResources();
            }
            catch (Exception ex)
            {
                using (ExceptionDialog dialog = new ExceptionDialog())
                {
                    dialog.Title = "An Exception Occurred During Unload Resources";

                    dialog.Label = ex.Message;
                    dialog.Detail = ex.ToString();

                    dialog.ShowDialog(this);
                }

                isInErrorState = true;
            }
        }

        private void GlControl1_Load(object sender, EventArgs e)
        {
            try
            {
                effects.LoadResources();

                imuModel.LoadResources();
                axisModel.LoadResources();
                floorTileModel.LoadResources();
                tileTexture.LoadResources();

                textRenderer.LoadResources();
                texturedQuad.LoadResources();

                //lightBuffer.LoadResources();
            }
            catch (Exception ex)
            {
                using (ExceptionDialog dialog = new ExceptionDialog())
                {
                    dialog.Title = "An Exception Occurred During Load Resources";

                    dialog.Label = ex.Message;
                    dialog.Detail = ex.ToString();

                    dialog.ShowDialog(this);
                }

                isInErrorState = true;
            }
        }

        private void GlControl1_MouseClick(object sender, MouseEventArgs e)
        {
            /*
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu regContextMenu = new ContextMenu();
                regContextMenu.MenuItems.Add(new MenuItem("Centre View"));
                regContextMenu.MenuItems.Add(new MenuItem("Cancel Centred View"));

                regContextMenu.MenuItems[0].Click += new EventHandler(delegate
                    {
                        intQuaternion = quaternion; // new float[] { q[0], -q[1], -q[2], -q[3] };
                        intQuaternion.Conjugate();
                    });

                regContextMenu.MenuItems[1].Click += new EventHandler(delegate
                    {
                        intQuaternion = ImuApi.Maths.Quaternion.Identity; // new float[] { 1.0f, 0.0f, 0.0f, 0.0f };
                    });

                regContextMenu.Show(glControl1, new Point(e.X, e.Y));
            }
            */
        }

        private void GlControl1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            m_MousePressed = false;
            m_ViewPitch = 0;
            m_ViewYaw = 0;
            modelScale = 1f;
        }

        private void GlControl1_MouseDown(object sender, MouseEventArgs e)
        {
            m_MousePressed = true;
            m_LastMouseLocation = e.Location;
        }

        private void GlControl1_MouseMove(object sender, MouseEventArgs e)
        {
            if (m_MousePressed == false)
            {
                return;
            }

            m_ViewPitch += ((float)(m_LastMouseLocation.Y - e.Location.Y) / Height) * (float)Math.PI;
            m_ViewYaw += ((float)(m_LastMouseLocation.X - e.Location.X) / Width) * (float)Math.PI;

            m_ViewPitch = Math.Min(((float)Math.PI * 0.5f), Math.Max(-((float)Math.PI * 0.5f), m_ViewPitch));
            m_ViewYaw = (m_ViewYaw + ((float)Math.PI * 2f)) % ((float)Math.PI * 2f);

            m_LastMouseLocation = e.Location;
        }

        private void GlControl1_MouseUp(object sender, MouseEventArgs e)
        {
            m_MousePressed = false;
        }

        private void GlControl1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta < 0)
            {
                modelScale += 0.125f;
            }
            else
            {
                modelScale -= 0.125f;
            }

            modelScale = MathHelper.Clamp(modelScale, 0.25f, 5f);
        }

        /// <summary>
        /// Redraw cuboid polygons.
        /// </summary>
        private void GlControl1_Paint(object sender, PaintEventArgs e)
        {
            if (isInErrorState == true)
            {
                return;
            }

            try
            {
                if (WindowState == FormWindowState.Minimized)
                {
                    return;
                }

                EulerAngles euler = NgimuApi.Maths.Quaternion.ToEulerAngles(quaternion);

                NgimuApi.Maths.Quaternion quat = quaternion;

                // Apply transformation matrix to cuboid
                RotationMatrix rotationMatrix = NgimuApi.Maths.Quaternion.ToRotationMatrix(quat);

                try
                {
                    glControl1.MakeCurrent();
                }
                catch (OpenTK.Graphics.GraphicsContextException ex)
                {
                    DisposeOpenGLControl();

                    LoadOpenGLControl();

                    return;
                }

                view.Rotation = OpenTK.Quaternion.FromAxisAngle(OpenTK.Vector3.UnitY, m_ViewYaw) * OpenTK.Quaternion.FromAxisAngle(OpenTK.Vector3.UnitX, m_ViewPitch);

                view.UpdateProjection();

                foreach (ModelSceneLight sceneLight in sceneLights)
                {
                    sceneLight.Update();
                }

                GLState.ClearColor(new Color4(15, 15, 15, 255));

                GLState.CullFace(OpenTK.Graphics.OpenGL.CullFaceMode.Back);
                GLState.EnableCullFace = true;
                GLState.EnableDepthMask = true;
                GLState.EnableDepthTest = true;
                GLState.EnableBlend = true;

                GLState.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
                GLState.BlendEquation(BlendEquationMode.FuncAdd);
                GLState.ClearDepth(1.0f);
                GLState.Viewport = view.Viewport;

                GLState.ApplyAll(glControl1.Size);

                GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

                Matrix4 model = Matrix4.Identity;

                OpenTK.Quaternion inverse = OpenTK.Quaternion.Invert(new OpenTK.Quaternion(quaternion.X, quaternion.Y, quaternion.Z, quaternion.W));

                model *= Matrix4.CreateScale(1f / modelScale);
                model *= Matrix4.CreateScale(3f);
                model *= Matrix4.CreateFromQuaternion(inverse);
                model *= Matrix4.CreateRotationX(-MathHelper.PiOver2);

                OpenTK.Quaternion normalQuaternion = model.ExtractRotation();

                Matrix4 normal = Matrix4.CreateFromQuaternion(normalQuaternion);

                float Emissivity = 0f;
                float Alpha = 1f;

                OpenTK.Vector3 color = new OpenTK.Vector3(1f, 1f, 1f);
                Vector2 surface = new Vector2(1f - (Emissivity * Emissivity), Alpha);

                lightingArguments.NormalWorldMatrix = view.NormalWorld;
                lightingArguments.WorldMatrix = view.World;
                lightingArguments.ProjectionMatrix = view.Projection;

                for (int i = 0; i < sceneLights.Length; i++)
                {
                    lights[i] = sceneLights[i].ToLightInstance(ref view.World, ref view.NormalWorld);
                }

                ModelLightInstance light = lights[0];

                if (imuModelToolStripMenuItem.Checked == true)
                {
                    material.Render(ref model, ref normal, ref color, ref surface, null, lightingArguments, ref light, imuModel.Vertices);
                }

                if (earthToolStripMenuItem.Checked == true)
                {
                    Matrix4 tileModel = Matrix4.Identity;
                    Matrix4 tileModelNormal = Matrix4.Identity;

                    tileModel *= Matrix4.CreateScale(4f, 0.1f, 4f);
                    tileModel *= Matrix4.CreateTranslation(new OpenTK.Vector3(0, -3.5f, 0));
                    tileModel *= Matrix4.CreateScale(1f / modelScale);

                    color = new OpenTK.Vector3(1f, 1f, 1f);
                    surface = new Vector2(1f, 0.5f);

                    tileMaterial.Render(ref tileModel, ref tileModelNormal, ref color, ref surface, tileTexture, lightingArguments, ref light, floorTileModel.Vertices);
                    //material.Render(ref tileModel, ref tileModelNormal, ref color, ref surface, null, lightingArguments, ref light, floorTileModel.Vertices);
                }

                textRenderer.Clear(Color.Transparent);

                if (imuAxesToolStripMenuItem.Checked == true)
                {
                    RenderAxes(model, OpenTK.Vector3.Zero, inverse, normalQuaternion, normal, Emissivity, Alpha, color, surface, light, 1f, 3.5f);

                    Matrix4 earthMatrix = Matrix4.Identity;

                    OpenTK.Vector3 offset = UnProject(ref view.Projection, ref view.View, glControl1.Size, new Vector2(-0.75f, -0.75f));

                    //OpenTK.Vector3 offset = new OpenTK.Vector3(0.5f, 0.5f, 0.1f);

                    OpenTK.Quaternion earthInverse = OpenTK.Quaternion.Identity;
                    OpenTK.Quaternion earthNormal = OpenTK.Quaternion.Identity;

                    RenderAxes(earthMatrix, offset, earthInverse, earthNormal, earthMatrix, Emissivity, Alpha, color, surface, light, 0.025f, 3.75f);
                }

                if (eulerAnglesToolStripMenuItem.Checked == true)
                {
                    textRenderer.DrawString("Roll", m_BaseFont2, Brushes.LightGray, new PointF(20, 44), false);
                    textRenderer.DrawString("Pitch", m_BaseFont2, Brushes.LightGray, new PointF(20, 84), false);
                    textRenderer.DrawString("Yaw", m_BaseFont2, Brushes.LightGray, new PointF(20, 124), false);

                    int verticleOffset = 4;

                    textRenderer.DrawString(String.Format("{0,7:###0.0}", euler[0]) + "°", m_BaseFont, Brushes.LightGray, new PointF(80, 44 + verticleOffset), false);
                    textRenderer.DrawString(String.Format("{0,7:###0.0}", euler[1]) + "°", m_BaseFont, Brushes.LightGray, new PointF(80, 84 + verticleOffset), false);
                    textRenderer.DrawString(String.Format("{0,7:###0.0}", euler[2]) + "°", m_BaseFont, Brushes.LightGray, new PointF(80, 124 + verticleOffset), false);
                }

                textRenderer.Update();

                texturedQuad.Render(view);

                glControl1.SwapBuffers();
            }
            catch (Exception ex)
            {
                using (ExceptionDialog dialog = new ExceptionDialog())
                {
                    dialog.Title = "An Exception Occurred";

                    dialog.Label = ex.Message;
                    dialog.Detail = ex.ToString();

                    dialog.ShowDialog(this);
                }

                isInErrorState = true;
            }
        }

        /// <summary>
        /// Window resize event to adjusts perspective.
        /// </summary>
        private void GlControl1_SizeChanged(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                return;
            }

            view.Resize(glControl1.Bounds, glControl1.Bounds.Width, glControl1.Bounds.Height);
            view.UpdateProjection();

            textRenderer.Resize(glControl1.Bounds.Size);

            GLState.Viewport = view.Viewport;
            GLState.Apply(glControl1.Size);
        }

        private void IMUBoardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LoadMainModel("PCB", pcbModelPath) == true)
            {
                imuBoardToolStripMenuItem.Checked = true;
                imuHousingToolStripMenuItem.Checked = false;
                loadOBJToolStripMenuItem.Checked = false;
            }
        }

        private void IMUHousingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LoadMainModel("HOUSING", housingModelPath) == true)
            {
                imuBoardToolStripMenuItem.Checked = false;
                imuHousingToolStripMenuItem.Checked = true;
                loadOBJToolStripMenuItem.Checked = false;
            }
        }

        private bool LoadMainModel(string name, string path)
        {
            Mesh newModel = null;

            try
            {
                newModel = LoadModel(name, path, true);

                newModel.LoadResources();

                imuModel.UnloadResources();

                imuModel = newModel;

                return true;
            }
            catch (Exception ex)
            {
                newModel.UnloadResources();

                using (ExceptionDialog dialog = new ExceptionDialog())
                {
                    dialog.Title = "Could Not Load Object File";

                    dialog.Label = ex.Message;
                    dialog.Detail = ex.ToString();

                    dialog.ShowDialog(this);
                }

                return false;
            }
        }

        private Mesh LoadModel(string name, string path, bool normaliseAndCenterise)
        {
            MeshVertex[] vertices;

            WavefrontObjLoader.FlattenIndices(path, out vertices, normaliseAndCenterise);

            return new Mesh(name, vertices);
        }

        private void loadOBJToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                if (LoadMainModel("Custom", openFileDialog1.FileName) == true)
                {
                    imuBoardToolStripMenuItem.Checked = false;
                    imuHousingToolStripMenuItem.Checked = false;
                    loadOBJToolStripMenuItem.Checked = true;
                }
            }
        }

        private void LoadOpenGLControl()
        {
            glControl1 = new GLControl(new OpenTK.Graphics.GraphicsMode(32, 24, 0, 8))
            {
                Dock = DockStyle.Fill,
            };

            glControl1.Paint += GlControl1_Paint;
            glControl1.Resize += GlControl1_SizeChanged;
            //glControl1.KeyDown += glControl1_KeyDown;
            //glControl1.KeyPress += glControl1_KeyPress;
            //glControl1.KeyUp += glControl1_KeyUp;
            //glControl1.MouseClick += glControl1_MouseClick;
            glControl1.MouseDoubleClick += GlControl1_MouseDoubleClick;
            glControl1.MouseDown += GlControl1_MouseDown;
            glControl1.MouseMove += GlControl1_MouseMove;
            glControl1.MouseUp += GlControl1_MouseUp;
            glControl1.MouseWheel += GlControl1_MouseWheel;

            glControl1.Load += GlControl1_Load;
            glControl1.Disposed += GlControl1_Disposed;

            Controls.Add(glControl1);
        }

        private void RenderAxes(Matrix4 model, OpenTK.Vector3 offset, OpenTK.Quaternion inverse, OpenTK.Quaternion normalQuaternion, Matrix4 normal,
            float Emissivity, float Alpha, OpenTK.Vector3 color, Vector2 surface, ModelLightInstance light, float scale, float letterDistance)
        {
            GL.Clear(ClearBufferMask.DepthBufferBit);

            Emissivity = 0.8f;
            Alpha = 0.5f;

            model = Matrix4.Identity;
            model *= Matrix4.CreateScale(0.4f);
            model *= Matrix4.CreateRotationY(-MathHelper.Pi);
            model *= Matrix4.CreateRotationZ(-MathHelper.PiOver2);

            Matrix4 baseMatrix = model;

            Matrix4 worldProjectionMatrix = view.World * view.Projection;

            //float letterDistance = 3.5f;

            {
                model = baseMatrix;
                model *= Matrix4.CreateFromQuaternion(inverse);
                model *= Matrix4.CreateRotationX(-MathHelper.PiOver2);
                model *= Matrix4.CreateTranslation(offset);

                normalQuaternion = model.ExtractRotation();

                normal = Matrix4.CreateFromQuaternion(normalQuaternion);

                color = new OpenTK.Vector3(1f, 0f, 0f);
                surface = new Vector2(1f - (Emissivity * Emissivity), Alpha);

                material.Render(ref model, ref normal, ref color, ref surface, null, lightingArguments, ref light, axisModel.Vertices);

                OpenTK.Vector3 textLocation = OpenTK.Vector3.Transform(new OpenTK.Vector3(0, letterDistance, 0), model);

                DrawText(ref worldProjectionMatrix, ref textLocation, "X", redBrush);
            }

            {
                model = baseMatrix;
                model *= Matrix4.CreateRotationZ(MathHelper.PiOver2);
                model *= Matrix4.CreateFromQuaternion(inverse);
                model *= Matrix4.CreateRotationX(-MathHelper.PiOver2);
                model *= Matrix4.CreateTranslation(offset);

                normalQuaternion = model.ExtractRotation();

                normal = Matrix4.CreateFromQuaternion(normalQuaternion);

                color = new OpenTK.Vector3(0f, 1f, 0f);
                surface = new Vector2(1f - (Emissivity * Emissivity), Alpha);

                material.Render(ref model, ref normal, ref color, ref surface, null, lightingArguments, ref light, axisModel.Vertices);

                OpenTK.Vector3 textLocation = OpenTK.Vector3.Transform(new OpenTK.Vector3(0, letterDistance, 0), model);

                DrawText(ref worldProjectionMatrix, ref textLocation, "Y", greenBrush);
            }

            {
                model = baseMatrix;
                model *= Matrix4.CreateRotationY(-MathHelper.PiOver2);
                model *= Matrix4.CreateFromQuaternion(inverse);
                model *= Matrix4.CreateRotationX(-MathHelper.PiOver2);
                model *= Matrix4.CreateTranslation(offset);

                normalQuaternion = model.ExtractRotation();

                normal = Matrix4.CreateFromQuaternion(normalQuaternion);

                color = new OpenTK.Vector3(0f, 0f, 1f);
                surface = new Vector2(1f - (Emissivity * Emissivity), Alpha);

                material.Render(ref model, ref normal, ref color, ref surface, null, lightingArguments, ref light, axisModel.Vertices);

                OpenTK.Vector3 textLocation = OpenTK.Vector3.Transform(new OpenTK.Vector3(0, letterDistance, 0), model);

                DrawText(ref worldProjectionMatrix, ref textLocation, "Z", blueBrush);
            }
        }

        private PointF ToScreenPoint(float x, float y, SizeF size)
        {
            y *= -1;

            x *= glControl1.Width * 0.5f;
            y *= glControl1.Height * 0.5f;

            x += glControl1.Width * 0.5f;
            y += glControl1.Height * 0.5f;

            y += size.Height * 0.1f;

            return new PointF(x, y);
        }

        #region Window Resize / Move Events

        private void Form_Resize(object sender, EventArgs e)
        {
            if (WindowState != FormWindowState.Minimized)
            {
                Options.Windows[ID].WindowState = WindowState;
            }
        }

        private void Form_ResizeBegin(object sender, EventArgs e)
        {
        }

        private void Form_ResizeEnd(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                Options.Windows[ID].Bounds = this.DesktopBounds;
            }
        }

        private void Form3DView_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        #endregion Window Resize / Move Events
    }
}