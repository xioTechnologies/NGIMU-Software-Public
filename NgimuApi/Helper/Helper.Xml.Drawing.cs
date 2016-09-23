using System;
using System.Drawing;
using System.Globalization;
using System.Xml;

namespace NgimuApi
{
    public static partial class Helper
    {
        public static void AppendAttributeAndValue(XmlElement element, string name, Rectangle value) { AppendAttributeAndValue(element, name, SerializeRectangle(value)); }
        public static void AppendAttributeAndValue(XmlElement element, string name, RectangleF value) { AppendAttributeAndValue(element, name, SerializeRectangleF(value)); }

        public static void AppendAttributeAndValue(XmlElement element, string name, PointF value) { AppendAttributeAndValue(element, name, SerializePointF(value)); }
        public static void AppendAttributeAndValue(XmlElement element, string name, Point value) { AppendAttributeAndValue(element, name, SerializePoint(value)); }

        public static void AppendAttributeAndValue(XmlElement element, string name, Size value) { AppendAttributeAndValue(element, name, Helper.SerializeSize(value)); }
        public static void AppendAttributeAndValue(XmlElement element, string name, SizeF value) { AppendAttributeAndValue(element, name, Helper.SerializeSizeF(value)); }


        public static Rectangle GetAttributeValue(XmlNode node, string name, Rectangle @default)
        {
            if (node.Attributes[name] == null)
            {
                return @default;
            }

            try
            {
                return DeserializeRectangle(node.Attributes[name].Value);
            }
            catch
            {
                return @default;
            }
        }

        public static RectangleF GetAttributeValue(XmlNode node, string name, RectangleF @default)
        {
            if (node.Attributes[name] == null)
            {
                return @default;
            }

            try
            {
                return DeserializeRectangleF(node.Attributes[name].Value);
            }
            catch
            {
                return @default;
            }
        }


        public static Size GetAttributeValue(XmlNode node, string name, Size @default)
        {
            if (node.Attributes[name] == null)
            {
                return @default;
            }

            try
            {
                return DeserializeSize(node.Attributes[name].Value);
            }
            catch
            {
                return @default;
            }
        }

        public static SizeF GetAttributeValue(XmlNode node, string name, SizeF @default)
        {
            if (node.Attributes[name] == null)
            {
                return @default;
            }

            try
            {
                return DeserializeSizeF(node.Attributes[name].Value);
            }
            catch
            {
                return @default;
            }
        }

        public static Point GetAttributeValue(XmlNode node, string name, Point @default)
        {
            if (node.Attributes[name] == null)
            {
                return @default;
            }

            try
            {
                return DeserializePoint(node.Attributes[name].Value);
            }
            catch
            {
                return @default;
            }
        }

        public static PointF GetAttributeValue(XmlNode node, string name, PointF @default)
        {
            if (node.Attributes[name] == null)
            {
                return @default;
            }

            try
            {
                return DeserializePointF(node.Attributes[name].Value);
            }
            catch
            {
                return @default;
            }
        }

        #region Rectangle Helpers

        public static string SerializeRectangle(Rectangle rect)
        {
            return String.Format("{0},{1},{2},{3}",
                rect.X.ToString(CultureInfo.InvariantCulture),
                rect.Y.ToString(CultureInfo.InvariantCulture),
                rect.Width.ToString(CultureInfo.InvariantCulture),
                rect.Height.ToString(CultureInfo.InvariantCulture));
        }

        public static Rectangle DeserializeRectangle(string str)
        {
            string[] pieces = str.Split(new char[] { ',' });
            int x, y, width, height;

            x = int.Parse(pieces[0], CultureInfo.InvariantCulture);
            y = int.Parse(pieces[1], CultureInfo.InvariantCulture);
            width = int.Parse(pieces[2], CultureInfo.InvariantCulture);
            height = int.Parse(pieces[3], CultureInfo.InvariantCulture);

            return new Rectangle(x, y, width, height);
        }

        public static string SerializeRectangleF(System.Drawing.RectangleF rect)
        {
            return String.Format("{0},{1},{2},{3}",
                rect.X.ToString(CultureInfo.InvariantCulture),
                rect.Y.ToString(CultureInfo.InvariantCulture),
                rect.Width.ToString(CultureInfo.InvariantCulture),
                rect.Height.ToString(CultureInfo.InvariantCulture));
        }

        public static System.Drawing.RectangleF DeserializeRectangleF(string str)
        {
            string[] pieces = str.Split(new char[] { ',' });
            float x, y, width, height;

            x = float.Parse(pieces[0], CultureInfo.InvariantCulture);
            y = float.Parse(pieces[1], CultureInfo.InvariantCulture);
            width = float.Parse(pieces[2], CultureInfo.InvariantCulture);
            height = float.Parse(pieces[3], CultureInfo.InvariantCulture);

            return new System.Drawing.RectangleF(x, y, width, height);
        }

        #endregion

        #region Point Helpers

        public static string SerializePoint(Point point)
        {
            return String.Format("{0},{1}",
                point.X.ToString(CultureInfo.InvariantCulture),
                point.Y.ToString(CultureInfo.InvariantCulture));
        }

        public static Point DeserializePoint(string str)
        {
            string[] pieces = str.Split(new char[] { ',' });
            int x, y;

            x = int.Parse(pieces[0], CultureInfo.InvariantCulture);
            y = int.Parse(pieces[1], CultureInfo.InvariantCulture);

            return new Point(x, y);
        }

        public static string SerializePointF(PointF point)
        {
            return String.Format("{0},{1}",
                point.X.ToString(CultureInfo.InvariantCulture),
                point.Y.ToString(CultureInfo.InvariantCulture));
        }

        public static PointF DeserializePointF(string str)
        {
            string[] pieces = str.Split(new char[] { ',' });
            float x, y;

            x = float.Parse(pieces[0], CultureInfo.InvariantCulture);
            y = float.Parse(pieces[1], CultureInfo.InvariantCulture);

            return new PointF(x, y);
        }

        #endregion

        #region Size Helpers

        public static string SerializeSize(Size size)
        {
            return String.Format("{0},{1}",
                size.Width.ToString(CultureInfo.InvariantCulture),
                size.Height.ToString(CultureInfo.InvariantCulture));
        }

        public static Size DeserializeSize(string str)
        {
            string[] pieces = str.Split(new char[] { ',' });
            int x, y;

            x = int.Parse(pieces[0], CultureInfo.InvariantCulture);
            y = int.Parse(pieces[1], CultureInfo.InvariantCulture);

            return new Size(x, y);
        }

        public static string SerializeSizeF(SizeF size)
        {
            return String.Format("{0},{1}",
                size.Width.ToString(CultureInfo.InvariantCulture),
                size.Height.ToString(CultureInfo.InvariantCulture));
        }

        public static SizeF DeserializeSizeF(string str)
        {
            string[] pieces = str.Split(new char[] { ',' });
            float x, y;

            x = float.Parse(pieces[0], CultureInfo.InvariantCulture);
            y = float.Parse(pieces[1], CultureInfo.InvariantCulture);

            return new SizeF(x, y);
        }

        #endregion
    }
}
