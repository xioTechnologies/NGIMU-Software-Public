namespace NgimuForms.DialogsAndWindows
{
    partial class BaseForm
    {
        /// <summary>
        /// Required designer command.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region m_Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BaseForm));
            this.SuspendLayout();
            // 
            // BaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(466, 352);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BaseForm";
            this.Text = "BaseForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BaseForm_FormClosing);
            this.Load += new System.EventHandler(this.BaseForm_Load);
            this.ResizeBegin += new System.EventHandler(this.BaseForm_ResizeBegin);
            this.ResizeEnd += new System.EventHandler(this.BaseForm_ResizeEnd);
            this.SizeChanged += new System.EventHandler(this.BaseForm_SizeChanged);
            this.VisibleChanged += new System.EventHandler(this.BaseForm_VisibleChanged);
            this.Resize += new System.EventHandler(this.BaseForm_Resize);
            this.ResumeLayout(false);

        }

        #endregion
    }
}