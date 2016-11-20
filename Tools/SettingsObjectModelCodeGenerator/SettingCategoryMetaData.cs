using System.Collections.Generic;

namespace SettingsObjectModelCodeGenerator
{
    /// <summary>
    /// A group of settings. 
    /// </summary>
    class SettingCategoryMetaData : ISettingMetaData
    {
        internal List<SettingValue> variables = new List<SettingValue>();
        internal List<ISettingMetaData> items = new List<ISettingMetaData>();

        /// <summary>
        /// Gets all the variables in this group. 
        /// </summary>
        public IEnumerable<SettingValue> Variables { get { return variables; } }

        /// <summary>
        /// Gets all the settings in this group. 
        /// </summary>
        public IEnumerable<ISettingMetaData> Items { get { return items; } }

        /// <summary>
        /// Is this a group. 
        /// </summary>
        public bool IsGroup { get { return true; } }

        /// <summary>
        /// Gets the name of this setting group. 
        /// </summary>
        public string UIText { get; set; }

        public string MemberName { get; set; }

        public string CategoryFullMemberName { get; set; }

        /// <summary>
        /// Gets the text of the category. 
        /// </summary>
        public string CategoryText { get { return new string('\t', CategoryPrefix) + Category; } }

        /// <summary>
        /// Gets the name of the category. 
        /// </summary>
        public string Category { get; set; }

        public int CategoryPrefix { get; set; }

        /// <summary>
        /// Is this category hidden. 
        /// </summary>
        public bool IsHidden { get; set; }
    }
}
