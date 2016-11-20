namespace SettingsObjectModelCodeGenerator
{
    /// <summary>
    /// Setting item. 
    /// </summary>
    interface ISettingMetaData
    {
        /// <summary>
        /// Is this item a group or items. 
        /// </summary>
        bool IsGroup { get; }

        /// <summary>
        /// Gets the text of the setting. 
        /// </summary>
        string UIText { get; }

        /// <summary>
        /// Gets the MemberName of the setting. 
        /// </summary>
        string MemberName { get; }

        /// <summary>
        /// Gets the category of the setting. 
        /// </summary>
        string Category { get; }

        /// <summary>
        /// Gets the text for the category. 
        /// </summary>
        string CategoryText { get; }
    }
}
