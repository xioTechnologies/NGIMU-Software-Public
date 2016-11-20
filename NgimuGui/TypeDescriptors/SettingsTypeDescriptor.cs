using System;
using System.Collections.Generic;
using System.ComponentModel;
using NgimuApi;

namespace NgimuGui.TypeDescriptors
{
    public class SettingsTypeDescriptor : ICustomTypeDescriptor
    {
        internal List<PropertyDescriptor> m_Properties;

        public SettingsTypeDescriptor(SettingCategrory category)
        {
            m_Properties = new List<PropertyDescriptor>();

            if (category is Settings)
            {
                foreach (ISettingItem item in category.Items)
                {
                    if (item.IsHidden == true)
                    {
                        continue;
                    }

                    if (item is ISettingValue)
                    {
                        m_Properties.Add(new SettingValuePropertyInfo(item as ISettingValue));
                    }
                    else if (item is SettingCategrory)
                    {
                        foreach (ISettingItem sub in (item as SettingCategrory).Items)
                        {
                            if (sub.IsHidden == true)
                            {
                                continue;
                            }

                            if (sub is ISettingValue)
                            {
                                m_Properties.Add(new SettingValuePropertyInfo(sub as ISettingValue));
                            }
                            else
                            {
                                m_Properties.Add(new SettingCategoryPropertyDescriptor(sub as SettingCategrory));
                            }
                        }
                    }
                }
            }
            else
            {
                foreach (ISettingItem item in category.Items)
                {
                    if (item.IsHidden == true)
                    {
                        continue;
                    }

                    if (item is ISettingValue)
                    {
                        m_Properties.Add(new SettingValuePropertyInfo(item as ISettingValue));
                    }
                    else if (item is SettingCategrory)
                    {
                        m_Properties.Add(new SettingCategoryPropertyDescriptor(item as SettingCategrory));
                    }
                }
            }
        }

        AttributeCollection ICustomTypeDescriptor.GetAttributes()
        {
            return TypeDescriptor.GetAttributes(this, true);
        }

        string ICustomTypeDescriptor.GetClassName()
        {
            return TypeDescriptor.GetClassName(this, true);
        }

        string ICustomTypeDescriptor.GetComponentName()
        {
            return TypeDescriptor.GetComponentName(this, true);
        }

        TypeConverter ICustomTypeDescriptor.GetConverter()
        {
            return TypeDescriptor.GetConverter(this, true);
        }

        EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
        {
            return TypeDescriptor.GetDefaultEvent(this, true);
        }

        PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
        {
            return null;
        }

        object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
        {
            return TypeDescriptor.GetEditor(this, editorBaseType, true);
        }

        EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
        {
            return TypeDescriptor.GetEvents(this, true);
        }

        EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
        {
            return TypeDescriptor.GetEvents(this, attributes, true);
        }

        PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
        {
            return ((ICustomTypeDescriptor)this).GetProperties(new Attribute[0]);
        }

        PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
        {
            List<PropertyDescriptor> props = new List<PropertyDescriptor>();

            foreach (PropertyDescriptor property in m_Properties)
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

            return new PropertyDescriptorCollection(props.ToArray());
        }

        object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
        {
            return this;
        }
    }
}
