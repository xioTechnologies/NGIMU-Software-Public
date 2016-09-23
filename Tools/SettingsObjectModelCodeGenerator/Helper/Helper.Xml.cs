using System;
using System.Globalization;
using System.Xml;

namespace SettingsObjectModelCodeGenerator
{
    public static partial class Helper
	{
		#region Node helpers

		public static XmlNode FindChild(string name, XmlNode node)
		{
			XmlNode child = null;

			foreach (System.Xml.XmlNode sub in node.ChildNodes)
			{
				if (sub.Name == name)
				{
					child = sub;
					break;
				}
			}

			return child;
		}

		public static void AppendAttributeAndValue(XmlElement element, string name, bool value) { AppendAttributeAndValue(element, name, value.ToString(CultureInfo.InvariantCulture)); }
		public static void AppendAttributeAndValue(XmlElement element, string name, int value) { AppendAttributeAndValue(element, name, value.ToString(CultureInfo.InvariantCulture)); }
		public static void AppendAttributeAndValue(XmlElement element, string name, uint value) { AppendAttributeAndValue(element, name, value.ToString(CultureInfo.InvariantCulture)); }
		public static void AppendAttributeAndValue(XmlElement element, string name, short value) { AppendAttributeAndValue(element, name, value.ToString(CultureInfo.InvariantCulture)); }
		public static void AppendAttributeAndValue(XmlElement element, string name, ushort value) { AppendAttributeAndValue(element, name, value.ToString(CultureInfo.InvariantCulture)); }
		public static void AppendAttributeAndValue(XmlElement element, string name, float value) { AppendAttributeAndValue(element, name, value.ToString(CultureInfo.InvariantCulture)); }
		public static void AppendAttributeAndValue(XmlElement element, string name, double value) { AppendAttributeAndValue(element, name, value.ToString(CultureInfo.InvariantCulture)); }
		public static void AppendAttributeAndValue(XmlElement element, string name, decimal value) { AppendAttributeAndValue(element, name, value.ToString(CultureInfo.InvariantCulture)); }

		public static void AppendAttributeAndValue<TResult>(XmlElement element, string name, TResult value) where TResult : struct, IConvertible
		{
			AppendAttributeAndValue(element, name, value.ToString());
		}

		public static void AppendAttributeAndValue(XmlElement element, string name, string value)
		{
			if (IsNotNullOrEmpty(value))
			{
				element.Attributes.Append(element.OwnerDocument.CreateAttribute(name));
				element.Attributes[name].Value = value;
			}
		}

		public static TResult GetAttributeValue<TResult>(XmlNode node, string name, TResult @default) where TResult : struct, IConvertible
		{
			if (!typeof(TResult).IsEnum)
			{
				throw new NotSupportedException("TResult must be an Enum.");
			}

			if ((node.Attributes[name] != null))
			{
				try
				{
					return (TResult)Enum.Parse(typeof(TResult), node.Attributes[name].Value, true);
				}
				catch
				{
					return @default;
				}
			}
			else
			{
				return @default;
			}
		}

		public static bool IsAttributeValueTrue(XmlNode node, string name, bool @default)
		{
			if ((node.Attributes[name] != null))
				return Helper.IsTrueValue(node.Attributes[name].Value);
			else
				return @default;
		}

		public static string GetAttributeValue(XmlNode node, string name, string @default)
		{
			if ((node.Attributes[name] != null))
				return node.Attributes[name].Value;
			else
				return @default;
		}

		public static int GetAttributeValue(XmlNode node, string name, int @default)
		{
			int @return;

			if ((node.Attributes[name] != null) && int.TryParse(node.Attributes[name].Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out @return))
				return @return;
			else
				return @default;
		}

		public static uint GetAttributeValue(XmlNode node, string name, uint @default)
		{
			uint @return;

			if ((node.Attributes[name] != null) && uint.TryParse(node.Attributes[name].Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out @return))
				return @return;
			else
				return @default;
		}

		public static short GetAttributeValue(XmlNode node, string name, short @default)
		{
			short @return;

			if ((node.Attributes[name] != null) && short.TryParse(node.Attributes[name].Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out @return))
				return @return;
			else
				return @default;
		}

		public static ushort GetAttributeValue(XmlNode node, string name, ushort @default)
		{
			ushort @return;

			if ((node.Attributes[name] != null) && ushort.TryParse(node.Attributes[name].Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out @return))
				return @return;
			else
				return @default;
		}

		public static float GetAttributeValue(XmlNode node, string name, float @default)
		{
			float @return;

			if ((node.Attributes[name] != null) && float.TryParse(node.Attributes[name].Value, NumberStyles.Float, CultureInfo.InvariantCulture, out @return))
				return @return;
			else
				return @default;
		}

		public static double GetAttributeValue(XmlNode node, string name, double @default)
		{
			double @return;

			if ((node.Attributes[name] != null) && double.TryParse(node.Attributes[name].Value, NumberStyles.Float, CultureInfo.InvariantCulture, out @return))
				return @return;
			else
				return @default;
		}

		public static decimal GetAttributeValue(XmlNode node, string name, decimal @default)
		{
			decimal @return;

			if ((node.Attributes[name] != null) && decimal.TryParse(node.Attributes[name].Value, NumberStyles.Float, CultureInfo.InvariantCulture, out @return))
				return @return;
			else
				return @default;
		}

		public static bool GetAttributeValue(XmlNode node, string name, bool @default)
		{
			bool @return;

			if ((node.Attributes[name] != null) && bool.TryParse(node.Attributes[name].Value, out @return))
				return @return;
			else
				return @default;
		}

		#endregion

		public static bool IsTrueValue(string value)
		{
			if (IsNullOrEmpty(value))
				return false;

			string val = value.Trim();

			bool result = false;
			if (bool.TryParse(val, out result))
				return result;
			else if ("yes".Equals(val, StringComparison.InvariantCultureIgnoreCase))
				return true;
			else if ("1".Equals(val))
				return true;

			return false;
		}

		public static XmlElement CreateElement(XmlNode node, string tag)
		{
			XmlDocument doc = node.OwnerDocument;
			if (node is XmlDocument)
			{
				doc = (XmlDocument)node;
			}

			return doc.CreateElement(tag);
		}

		public static void AppendCData(XmlNode node, string tag, string content)
		{
			if (String.IsNullOrEmpty(content) == true)
			{
				return;
			}

			XmlDocument doc = node.OwnerDocument;
			if (node is XmlDocument)
			{
				doc = (XmlDocument)node;
			}

			XmlElement element = (XmlElement)node.AppendChild(doc.CreateElement(tag));

			element.AppendChild(doc.CreateCDataSection(content));
		}

		public static string ReadCData(XmlNode node, string tag)
		{
			XmlNode cdata = node.SelectSingleNode(tag);

			if (cdata == null)
			{
				return String.Empty;
			}

			return cdata.InnerText;
		}
	}
}