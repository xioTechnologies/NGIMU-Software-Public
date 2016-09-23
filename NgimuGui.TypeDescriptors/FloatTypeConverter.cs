using System;
using System.ComponentModel;
using System.Globalization;

namespace NgimuGui.TypeDescriptors
{

    public class FloatTypeConverter : TypeConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (value is float)
            {
                return ((float)value).ToString(CultureInfo.InvariantCulture);
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(float))
            {
                return true;
            }

            if (sourceType == typeof(string))
            {

                return true;
            }

            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            if (value is float)
            {
                return value;
            }

            if (value is string)
            {
                float floatValue;

                if (float.TryParse((string)value, NumberStyles.Float, CultureInfo.InvariantCulture, out floatValue) == true)
                {
                    return floatValue;
                }
            }

            return base.ConvertFrom(context, culture, value);
        }
    }
}
