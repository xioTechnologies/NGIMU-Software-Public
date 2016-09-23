using System;
using System.Collections.Generic;
using System.ComponentModel;
using NgimuApi;

namespace NgimuGui.TypeDescriptors
{
    public class SettingCategoryPropertyDescriptor : PropertyDescriptor
    {
        private SettingCategrory m_Category;

        internal SettingsTypeDescriptor m_Descriptor;

        /// <summary>
        /// The category that this category belongs to, i.e the parents category text
        /// </summary>
        public override string Category
        {
            get
            {
                if (m_Category.Parent != null)
                {
                    return m_Category.Parent.CategoryText;
                }
                else
                {
                    return null;
                }
            }
        }

        public SettingCategoryPropertyDescriptor(SettingCategrory group)
            : base(group.Text, new Attribute[0])
        {
            m_Category = group;
            m_Descriptor = new SettingsTypeDescriptor(group);
        }

        public void UpdateAttributes()
        {
            List<Attribute> propertyAttributes = new List<Attribute>();

            if (IsReadOnly == true)
            {
                propertyAttributes.Add(new ReadOnlyAttribute(true));
            }

            if (Category != null)
            {
                propertyAttributes.Add(new CategoryAttribute(Category));
            }

            if (Description != null)
            {
                propertyAttributes.Add(new DescriptionAttribute(Description));
            }

            propertyAttributes.Add(new TypeConverterAttribute(typeof(SettingCategoryExpander).AssemblyQualifiedName));

            AttributeArray = propertyAttributes.ToArray();
        }


        public override bool CanResetValue(object component)
        {
            return false;
        }

        public override Type ComponentType
        {
            get { return m_Category.GetType(); }
        }

        public override object GetValue(object component)
        {
            return this;
        }

        public override bool IsReadOnly
        {
            get { return true; }
        }

        public override Type PropertyType
        {
            get { return typeof(SettingCategoryPropertyDescriptor); }
        }

        public override void ResetValue(object component)
        {

        }

        public override void SetValue(object component, object value)
        {

        }

        public override bool ShouldSerializeValue(object component)
        {
            return false;
        }
    }
}
