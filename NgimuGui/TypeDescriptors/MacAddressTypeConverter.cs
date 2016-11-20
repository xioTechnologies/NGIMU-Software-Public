using System;
using System.ComponentModel;
using System.Globalization;
using System.Net.NetworkInformation;
using System.Text;

namespace NgimuGui.TypeDescriptors
{
    public class MacAddressTypeConverter : TypeConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (value is PhysicalAddress && destinationType == typeof(string))
            {
                PhysicalAddress address = value as PhysicalAddress;

                byte[] bytes = address.GetAddressBytes();

                StringBuilder sb = new StringBuilder(bytes.Length * 3);

                for (int i = 0; i < bytes.Length; i++)
                {
                    // Display the physical address in hexadecimal.
                    sb.AppendFormat("{0}", bytes[i].ToString("X2"));

                    // Insert a hyphen after each byte, unless we are at the end of the address.
                    if (i != bytes.Length - 1)
                    {
                        sb.Append("-");
                    }
                }

                return sb.ToString();
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }

            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            if (value is string)
            {
                return PhysicalAddress.Parse((string)value);
            }

            return base.ConvertFrom(context, culture, value);
        }
    }
}
