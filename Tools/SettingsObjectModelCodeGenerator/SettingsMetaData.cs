using System;
using System.Collections.Generic;
using System.Xml;

namespace SettingsObjectModelCodeGenerator
{
    /// <summary>
    /// Settings loaded from an embedded source. 
    /// </summary>
    class SettingsMetaData : SettingCategoryMetaData
	{
        private Dictionary<string, SettingValue> variableLookup = new Dictionary<string, SettingValue>();

        internal List<SettingValue> allVariables = new List<SettingValue>();

		/// <summary>
		/// Gets all the variables. 
		/// </summary>
        public IEnumerable<SettingValue> AllVariables { get { return allVariables; } }

		/// <summary>
		/// Gets the text to display when no setting is selected. This is a candidate to be moved out of the API. 
		/// </summary>
        public string NoItemSelectedText { get; private set; }

		/// <summary>
		/// Gets a variable by its OSC address. 
		/// </summary>
		/// <param name="address">A OSC address.</param>
		/// <returns>A remote variable.</returns>
        public SettingValue GetSetting(string address) { return variableLookup[address]; }

        /// <summary>
        /// Load the settings from the embedded source. 
        /// </summary>
        public SettingsMetaData(XmlDocument xml)
		{
            Load(xml); 
		}

		/// <summary>
		/// Does the settings contain a setting with a given OSC address.
		/// </summary>
		/// <param name="address">A OSC address.</param>
		/// <returns>True if a setting exists with the supplied OSC address.</returns>
        public bool ContainsAddress(string address)
		{
			return variableLookup.ContainsKey(address);
		}

		/// <summary>
		/// Gets all the variables in a category. 
		/// </summary>
		/// <param name="category">The name of a category.</param>
		/// <returns>A set of remote variables.</returns>
        public IEnumerable<SettingValue> VariablesInCategory(string category)
		{
			List<SettingValue> variables = new List<SettingValue>();

			foreach (SettingValue var in allVariables)
			{
				if (category.Equals(var.Category) == true)
				{
					variables.Add(var);
				}
			}

			return variables;
		}

        private void Load(XmlDocument doc)
		{
			try
			{
				XmlNode node = doc.DocumentElement;

				if (node == null)
				{
					throw new Exception("Missing root node.");	
				}

				NoItemSelectedText = node.SelectSingleNode("NoItemText").InnerText.Trim(); 

				List<string> categories = new List<string>(); 

				foreach (XmlNode categoryNode in node.SelectNodes("Category")) 
				{
                    string subCategoryMemberName = Helper.GetAttributeValue(categoryNode, "MemberName", "Uncategorised");
                    string subCategoryText = Helper.GetAttributeValue(categoryNode, "UIText", "Uncategorised");
                    bool isHidden = Helper.GetAttributeValue(categoryNode, "Hidden", false);

                    SettingCategoryMetaData settingGroup = new SettingCategoryMetaData()
                    {
                        UIText = subCategoryText,
                        MemberName = subCategoryMemberName,
                        Category = subCategoryText, // string.Empty
                        CategoryFullMemberName = subCategoryMemberName,
                        IsHidden = isHidden,
                    };

                    if (categories.Contains(subCategoryText) == false)
                    {
                        categories.Add(subCategoryText);
                    }    

                    List<string> categoriesList = new List<string>();

                    LoadCategory(settingGroup, categoriesList, categoryNode);

                    ApplyOrderToCategories(settingGroup, categoriesList);

                    this.items.Add(settingGroup);                    
				}

                ApplyOrderToCategories(this, categories);			
			}
			catch (Exception ex)
			{
				throw new Exception("Could not load IMU settings from XML.", ex); 
			}
		}

        private void LoadCategory(SettingCategoryMetaData category, List<string> categories, XmlNode node)
		{
			string categoryText = Helper.GetAttributeValue(node, "UIText", "Uncategorised");
			
			if (categories.Contains(categoryText) == false)
			{
				categories.Add(categoryText);
			}         

			foreach (XmlNode subNode in node.ChildNodes)
			{
				if (subNode.Name == "Setting")
				{
					XmlNode settingNode = subNode;

					SettingValueType type = Helper.GetAttributeValue(settingNode, "Type", (SettingValueType)(-1));

                    string memberName = Helper.GetAttributeValue(settingNode, "MemberName", null);
                    string uiText = Helper.GetAttributeValue(settingNode, "UIText", null);
                    string documentationTitle = Helper.GetAttributeValue(settingNode, "DocumentationTitle", uiText);

					string oscAddress = Helper.GetAttributeValue(settingNode, "OscAddress", null);
					bool isReadonly = Helper.GetAttributeValue(settingNode, "Readonly", false);
                    bool isHidden = Helper.GetAttributeValue(settingNode, "Hidden", false);

					string documentationBody = settingNode.InnerText.Trim();

                    if (Helper.IsNullOrEmpty(memberName) == true)
                    {
                        throw new Exception("One or more setting does not have a \"MemberName\" attribute.");
                    }

                    if (Helper.IsNullOrEmpty(uiText) == true)
					{
                        throw new Exception("One or more setting does not have a \"Text\" attribute.");
					}

					if (Helper.IsNullOrEmpty(oscAddress) == true)
					{
						throw new Exception("One or more setting does not have a \"OscAddress\" attribute.");
					}

                    SettingValue var = new SettingValue()
                    {
                        Category = categoryText,
                        MemberName = memberName, 
                        UIText = uiText, 
                        DocumentationTitle = documentationTitle, 
                        DocumentationBody = documentationBody, 
                        OscAddress = oscAddress, 
                        IsReadOnly = isReadonly,
                        IsHidden = isHidden, 
                        SettingValueType = type,
                        CategoryFullMemberName = category.CategoryFullMemberName,
                    };

					allVariables.Add(var);
					
					variableLookup.Add(var.OscAddress, var); 

					category.variables.Add(var);
					category.items.Add(var);
				}
				else if (subNode.Name == "Category")
				{
					XmlNode categoryNode = subNode;

                    string subCategoryMemberName = Helper.GetAttributeValue(categoryNode, "MemberName", "Uncategorised");
					string subCategoryText = Helper.GetAttributeValue(categoryNode, "UIText", "Uncategorised");
                    bool isHidden = Helper.GetAttributeValue(categoryNode, "Hidden", false);

                    SettingCategoryMetaData settingCategory = new SettingCategoryMetaData()
                    {
                        UIText = subCategoryText, 
                        MemberName = subCategoryMemberName, 
                        Category = categoryText,
                        CategoryFullMemberName = category.CategoryFullMemberName + "." + subCategoryMemberName,
                        IsHidden = isHidden,
                    };

					List<string> categoriesList = new List<string>();

					LoadCategory(settingCategory, categoriesList, categoryNode);

					ApplyOrderToCategories(settingCategory, categoriesList);

					category.items.Add(settingCategory);
				}				
			}
		}

        private void ApplyOrderToCategories(SettingCategoryMetaData group, List<string> categories)
		{
			int order = categories.Count + 1;
			foreach (string category in categories)
			{
				foreach (ISettingMetaData var in group.items)
				{
					if (var.Category != category)
					{
						continue;
					}

                    if (var is SettingValue)
					{
                        (var as SettingValue).CategoryPrefix = order;
					}
					else if (var is SettingCategoryMetaData)
					{
						(var as SettingCategoryMetaData).CategoryPrefix = order;
					}
				}

				order--;
			}
		}
	}
}
