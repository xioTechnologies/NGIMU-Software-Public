using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using NgimuApi;

namespace NgimuGui.TypeDescriptors
{
    public class SettingValuePropertyInfo : PropertyDescriptor
    {
        public ISettingValue Variable { get; private set; }

        public override string Category { get { return Variable.Category.CategoryText; } }

        public string ConverterTypeName { get; private set; }

        public object DefaultValue
        {
            get { return Variable.GetRemoteValue(); }
            set { Variable.SetRemoteValue(value); }
        }

        public override string Description { get { return Variable.Documentation; } }

        public string EditorTypeName { get; private set; }

        public override bool IsReadOnly { get { return Variable.IsReadOnly; } }

        public override Type PropertyType { get { return Variable.ValueType; } }

        public override Type ComponentType { get { return Variable.GetType(); } }

        public SettingValuePropertyInfo(ISettingValue var)
            : base(var.Name, new Attribute[0])
        {
            Variable = var;

            if (Variable.SettingValueType == SettingValueType.IPAddress)
            {
                ConverterTypeName = typeof(IPAddressTypeConverter).AssemblyQualifiedName;
            }

            if (Variable.SettingValueType == SettingValueType.MacAddress)
            {
                ConverterTypeName = typeof(MacAddressTypeConverter).AssemblyQualifiedName;
            }

            if (Variable.SettingValueType == SettingValueType.Float)
            {
                ConverterTypeName = typeof(FloatTypeConverter).AssemblyQualifiedName;
            }

            if (Variable.SettingValueType == SettingValueType.WifiMode ||
                Variable.SettingValueType == SettingValueType.Wifi2GHzChannel ||
                Variable.SettingValueType == SettingValueType.Wifi5GHzChannel ||
                Variable.SettingValueType == SettingValueType.WifiAntenna ||
                Variable.SettingValueType == SettingValueType.WifiRegion ||
                Variable.SettingValueType == SettingValueType.WifiBand ||
                Variable.SettingValueType == SettingValueType.OscPassthroughMode || 
                Variable.SettingValueType == SettingValueType.CpuIdleMode)
            {
                ConverterTypeName = typeof(CustomEnumConverter).AssemblyQualifiedName;
            }
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

            if (EditorTypeName != null)
            {
                propertyAttributes.Add(new EditorAttribute(EditorTypeName, typeof(UITypeEditor)));
            }

            if (ConverterTypeName != null)
            {
                propertyAttributes.Add(new TypeConverterAttribute(ConverterTypeName));
            }

            AttributeArray = propertyAttributes.ToArray();
        }

        public override bool CanResetValue(object component)
        {
            if (DefaultValue == null)
            {
                return false;
            }
            else
            {
                return !this.GetValue(component).Equals(DefaultValue);
            }
        }

        public override object GetValue(object component)
        {
            return Variable.GetValue();
        }

        public override void ResetValue(object component)
        {
            SetValue(component, DefaultValue);
        }

        public override void SetValue(object component, object value)
        {
            Variable.SetValue(value);
        }

        public override bool ShouldSerializeValue(object component)
        {
            object val = this.GetValue(component);

            if (DefaultValue == null && val == null)
            {
                return false;
            }
            else
            {
                return !val.Equals(DefaultValue);
            }
        }
    }
}
