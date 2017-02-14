using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using NgimuApi;

namespace NgimuForms
{
    public class WindowOptions
    {
        /// <summary>
        /// Is the window currently open.
        /// </summary>
        public bool IsOpen { get; set; }

        /// <summary>
        /// The current state of the window (maximised or normal).
        /// </summary>
        public FormWindowState WindowState { get; set; }

        /// <summary>
        /// The bounds of the window on the desktop.
        /// </summary>
        public Rectangle Bounds { get; set; }
    }

    public static class WindowManager
    {
        private static readonly Dictionary<string, WindowOptions> Windows = new Dictionary<string, WindowOptions>();

        public static ICollection<string> Keys => Windows.Keys;

        public static void Clear()
        {
            Windows.Clear();
        }

        public static bool Contains(string key)
        {
            return Windows.ContainsKey(key);
        }

        public static WindowOptions Get(string key)
        {
            if (Contains(key) == false)
            {
                Windows.Add(key, new WindowOptions());
            }

            return Windows[key];
        }

        public static void Load(XmlNode parent)
        {
            foreach (XmlNode windowNode in parent.SelectNodes("Window"))
            {
                string name = Helper.GetAttributeValue(windowNode, "Name", null);

                if (string.IsNullOrEmpty(name) == true)
                {
                    continue;
                }

                if (Contains(name) == true)
                {
                    continue;
                }

                WindowOptions windowOptions = Get(name);

                // get the open state
                windowOptions.IsOpen = Helper.GetAttributeValue(windowNode, "IsOpen", windowOptions.IsOpen);

                // get the window state
                windowOptions.WindowState = Helper.GetAttributeValue(windowNode, "WindowState", windowOptions.WindowState);

                // get the bounding rectangle
                windowOptions.Bounds = CheckWindowBounds(Helper.GetAttributeValue(windowNode, "Bounds", windowOptions.Bounds));
            }
        }

        public static void Save(XmlNode parent)
        {
            XmlElement windows = Helper.CreateElement(parent, "Windows");

            foreach (string name in Keys)
            {
                WindowOptions options = Get(name);

                XmlElement window = Helper.CreateElement(windows, "Window");

                Helper.AppendAttributeAndValue(window, "Name", name);

                Helper.AppendAttributeAndValue(window, "IsOpen", options.IsOpen);

                Helper.AppendAttributeAndValue(window, "WindowState", options.WindowState);

                Helper.AppendAttributeAndValue(window, "Bounds", options.Bounds);

                windows.AppendChild(window);
            }

            parent.AppendChild(windows);
        }

        /// <summary>
        /// Check that a rectangle is fully on the screen
        /// </summary>
        /// <param name="rectangle">the rectangle to check</param>
        /// <returns>true if the rectangle is fully on a screen</returns>
        public static bool IsOnScreen(Rectangle rectangle)
        {
            return Screen.AllScreens.Any(screen => screen.WorkingArea.Contains(rectangle));
        }

        public static Rectangle CheckWindowBounds(Rectangle bounds)
        {
            // if the bounds is not empty
            if (bounds == Rectangle.Empty)
            {
                return bounds;
            }

            // check that the bounds is on the screen
            if (IsOnScreen(bounds) == false)
            {
                // if the bounds is off the screen set it to empty
                bounds = Rectangle.Empty;
            }

            return bounds;
        }
    }
}