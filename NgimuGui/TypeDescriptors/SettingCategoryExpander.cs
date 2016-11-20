using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace NgimuGui.TypeDescriptors
{
    public class SettingCategoryExpander : ExpandableObjectConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            return "";
        }

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            List<PropertyDescriptor> props = new List<PropertyDescriptor>();

            if (value is SettingCategoryPropertyDescriptor)
            {
                SettingCategoryPropertyDescriptor group = value as SettingCategoryPropertyDescriptor;

                foreach (PropertyDescriptor property in group.m_Descriptor.m_Properties)
                {
                    if (property is SettingValuePropertyInfo)
                    {
                        (property as SettingValuePropertyInfo).UpdateAttributes();
                    }

                    if (property is SettingCategoryPropertyDescriptor)
                    {
                        (property as SettingCategoryPropertyDescriptor).UpdateAttributes();
                    }

                    props.Add(property);
                }
            }

            return new PropertyDescriptorCollection(props.ToArray());
        }
    }
}
