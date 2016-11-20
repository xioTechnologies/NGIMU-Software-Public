using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using Rug.Cmd;
using Rug.Cmd.Colors;

namespace SettingsObjectModelCodeGenerator
{
    class Program
    {
        static string FileTemplate;
        static string CategoryTemplate;
        static string SettingsTemplate;

        static void Main(string[] args)
        {
            ConsoleColorState state = RC.ColorState;

            // create the argument parser
            ArgumentParser parser = new ArgumentParser("SettingsObjectModelCodeGenerator", "Generates settings object model code for the ImuApi project");

            StringArgument XmlArg = new StringArgument("XML", "The path of the settings XML file", "The path of the settings XML file");
            StringArgument CodeArg = new StringArgument("Code", "The destination code .cs file", "The destination code .cs file");
            StringArgument DocumentationArg = new StringArgument("Documentation", "The documentation .XML file", "The documentation .XML file");

            // add the arguments to the parser 
            parser.Add("/", "XML", XmlArg);
            parser.Add("/", "Code", CodeArg);
            parser.Add("/", "Documentation", DocumentationArg);

            try
            {
                RC.IsBuildMode = true;
                RC.Verbosity = ConsoleVerbosity.Debug;
                RC.ApplicationBuildReportPrefix = "SGEN";

                RC.Theme = ConsoleColorTheme.Load(ConsoleColorDefaultThemes.Colorful);

                RC.WriteLine(ConsoleThemeColor.TitleText, "Settings Object Model Code Generator");

                // parse arguemnts
                parser.Parse(args);

                // did the parser detect a /? arguemnt 
                if (parser.HelpMode == true)
                {
                    return;
                }

                if (XmlArg.Defined == false)
                {
                    RC.WriteError(001, "No settings XML file supplied.");
                    return;
                }

                if (CodeArg.Defined == false)
                {
                    RC.WriteError(001, "No destination file supplied.");
                    return;
                }

                if (DocumentationArg.Defined == false)
                {
                    RC.WriteError(001, "No documentation file supplied.");
                    return;
                }

                string xmlFilePath = XmlArg.Value;

                if (File.Exists(xmlFilePath) == false)
                {
                    RC.WriteError(002, "Settings XML file does not exist.");
                    return;
                }

                string codeFilePath = CodeArg.Value;

                try
                {
                    FileInfo info = new FileInfo(codeFilePath);
                }
                catch
                {
                    RC.WriteError(002, "Settings destination file is not valid.");
                    return;
                }

                string documentationFilePath = DocumentationArg.Value;

                try
                {
                    FileInfo info = new FileInfo(documentationFilePath);
                }
                catch
                {
                    RC.WriteError(002, "Settings documentation file is not valid.");
                    return;
                }

                Generate(xmlFilePath, codeFilePath, documentationFilePath);
            }
            catch (Exception ex)
            {
                RC.WriteException(04, System.Reflection.Assembly.GetExecutingAssembly().Location, 0, 0, ex);
            }
            finally
            {
                //RC.PromptForKey("Press any key to exit..", true, false);

                RC.ColorState = state;
            }
        }

        private static void Generate(string xmlFilePath, string codeFilePath, string documentationFilePath)
        {
            RC.WriteLine(ConsoleThemeColor.TitleText, "Reading XML File");

            XmlDocument doc = new XmlDocument();

            doc.Load(xmlFilePath);

            SettingsMetaData settings = new SettingsMetaData(doc);

            FileTemplate = File.ReadAllText(Helper.ResolvePath("~/Templates/FileTemplate.txt"));
            CategoryTemplate = File.ReadAllText(Helper.ResolvePath("~/Templates/CategoryTemplate.txt"));
            SettingsTemplate = File.ReadAllText(Helper.ResolvePath("~/Templates/SettingsTemplate.txt"));

            List<string> groupTypesDefinitions = new List<string>();

            string settingsTypeDefinition = GenerateSettingsType(settings, ref groupTypesDefinitions);

            string final = FileTemplate
                .Replace("[$RootCategory]", PrefixLines(new string[] { settingsTypeDefinition }, '\t', 1))
                .Replace("[$CategoryClasses]", PrefixLines(groupTypesDefinitions, '\t', 2));

            RC.WriteLine(ConsoleThemeColor.TitleText, "Writing: " + codeFilePath);
            File.WriteAllText(codeFilePath, final);

            RC.WriteLine(ConsoleThemeColor.TitleText, "Writing: " + documentationFilePath);

            WriteDocumentation(documentationFilePath, settings);
        }

        private static void WriteDocumentation(string documentationFilePath, SettingsMetaData settings)
        {
            XmlDocument doc = new XmlDocument();

            doc.AppendChild(doc.CreateComment(" THIS FILE WAS GENERATED AUTOMATICALLY. DO NOT MODIFY. "));

            XmlNode root = doc.AppendChild(Helper.CreateElement(doc, "Documentation"));

            {
                RC.WriteLine(ConsoleThemeColor.TitleText, " - " + "NoItemSelectedText");

                XmlElement metaNode = Helper.CreateElement(doc, "NoItemSelectedText");

                metaNode.InnerText = settings.NoItemSelectedText;

                root.AppendChild(metaNode);
            }

            foreach (SettingValue meta in settings.AllVariables)
            {
                RC.WriteLine(ConsoleThemeColor.TitleText, " - " + meta.OscAddress);

                XmlElement metaNode = Helper.CreateElement(doc, "Setting");

                Helper.AppendAttributeAndValue(metaNode, "Address", meta.OscAddress);

                metaNode.InnerText = meta.DocumentationBody;

                root.AppendChild(metaNode);
            }

            doc.Save(documentationFilePath);
        }

        private static string GenerateSettingsType(SettingsMetaData settings, ref List<string> groupTypesDefinitions)
        {
            Dictionary<string, string> replacements = new Dictionary<string, string>();

            GenerateMembers(settings, true, groupTypesDefinitions, replacements, "Settings");

            return DoReplacements(replacements, SettingsTemplate);
        }

        private static void GenerateCategoryType(string parentClassName, SettingCategoryMetaData category, ref List<string> groupTypesDefinitions)
        {
            Dictionary<string, string> replacements = new Dictionary<string, string>();

            replacements.Add("[$ParentCategroryClassName]", parentClassName);

            GenerateMembers(category, false, groupTypesDefinitions, replacements, category.MemberName);

            groupTypesDefinitions.Add(DoReplacements(replacements, CategoryTemplate));
        }

        private static string DoReplacements(Dictionary<string, string> replacements, string template)
        {
            foreach (string key in replacements.Keys)
            {
                template = template.Replace(key, replacements[key]);
            }

            return template;
        }

        private static void GenerateMembers(SettingCategoryMetaData category, bool isRoot, List<string> groupTypesDefinitions, Dictionary<string, string> replacements, string className)
        {
            RC.WriteLine(ConsoleThemeColor.TitleText, " - " + category.UIText);

            StringBuilder Members = new StringBuilder();
            StringBuilder Members_Initiation = new StringBuilder();
            StringBuilder Settings_Initiation = new StringBuilder();
            StringBuilder Members_Attach = new StringBuilder();
            StringBuilder Members_Copy = new StringBuilder();

            List<ISettingMetaData> items = new List<ISettingMetaData>();

            items.AddRange(category.Items);

            // if we are in the root then all the settings
            if (isRoot == true)
            {
                foreach (SettingValue value in (category as SettingsMetaData).AllVariables)
                {
                    items.Add(value);
                }
            }

            foreach (ISettingMetaData item in items)
            {
                if (item is SettingValue)
                {
                    SettingValue value = item as SettingValue;

                    GenerateSettingMember(Members, value);

                    if (isRoot == true)
                    {
                        GenerateSettingMemberConstruction(Settings_Initiation, value);
                    }
                    else
                    {
                        GenerateSettingMemberAttach(Members_Attach, value);
                        GenerateSettingMemberCopy(Members_Copy, value);
                    }
                }
                else if (item is SettingCategoryMetaData)
                {
                    SettingCategoryMetaData subCategory = item as SettingCategoryMetaData;

                    GenerateCategoryType(className, subCategory, ref groupTypesDefinitions);

                    GenerateCategoryMember(Members, subCategory);

                    GenerateCategoryContruction(Members_Initiation, subCategory);

                    GenerateCategoryCopy(Members_Copy, subCategory);
                }
            }

            if (Settings_Initiation.Length > 0)
            {
                Members_Initiation.AppendLine();
                Members_Initiation.Append(Settings_Initiation.ToString());
            }

            foreach (ISettingMetaData item in category.Items)
            {
                if (item is SettingCategoryMetaData)
                {
                    Members_Attach.AppendLine(string.Format("{0}.AttachSettings(settings);", item.MemberName));
                }
            }

            Members_Attach.AppendLine();

            foreach (ISettingMetaData item in category.Items)
            {
                if (item is SettingCategoryMetaData)
                {
                    //if ((isRoot == false) || (item as ImuSettingCategoryMetaData).IsHidden == false))
                    {
                        Members_Attach.AppendLine(string.Format("Add({0});", item.MemberName));
                    }
                }
                else if (isRoot == false)
                {
                    Members_Attach.AppendLine(string.Format("Add(settings.{0});", item.MemberName));
                }
                else
                {
                    // do nothing? 
                }
            }

            replacements.Add("[$CategroryClassName]", className);
            replacements.Add("[$CategroryText]", category.UIText);
            replacements.Add("[$CategroryIsHidden]", category.IsHidden.ToString().ToLowerInvariant());
            replacements.Add("[$CategoryPrefix]", category.CategoryPrefix.ToString());
            replacements.Add("[$Members]", PrefixLines(new string[] { Members.ToString() }, '\t', 1));
            replacements.Add("[$Members_Initiation]", PrefixLines(new string[] { Members_Initiation.ToString() }, '\t', 2));
            replacements.Add("[$Members_Attach]", PrefixLines(new string[] { Members_Attach.ToString() }, '\t', 2));
            replacements.Add("[$Members_Copy]", PrefixLines(new string[] { Members_Copy.ToString() }, '\t', 2));
        }

        private static void GenerateCategoryCopy(StringBuilder Members_Copy, SettingCategoryMetaData subCategory)
        {
            Members_Copy.AppendLine(string.Format("{0}.CopyTo(other.{0});", subCategory.MemberName));
        }

        private static void GenerateCategoryContruction(StringBuilder Members_Initiation, SettingCategoryMetaData subCategory)
        {
            Members_Initiation.AppendLine(string.Format("{0} = new SettingsCategoryTypes.{0}(this);", subCategory.MemberName));
        }

        private static void GenerateCategoryMember(StringBuilder Members, SettingCategoryMetaData subCategory)
        {
            Members.AppendLine("/// <summary>");
            Members.AppendLine("/// Gets " + subCategory.UIText + " settings category.");
            Members.AppendLine("/// </summary>");
            Members.AppendLine(string.Format("public SettingsCategoryTypes.{0} {0} {{ get; private set; }}", subCategory.MemberName));
            Members.AppendLine();
        }

        private static void GenerateSettingMemberCopy(StringBuilder Members_Copy, SettingValue value)
        {
            Members_Copy.AppendLine(string.Format("if ({0}.IsValueUndefined == false) {{ other.{0}.Value = {0}.Value; }}", value.MemberName));
        }

        private static void GenerateSettingMemberConstruction(StringBuilder Members_Initiation, SettingValue value)
        {
            Members_Initiation.AppendLine(string.Format("{0} = new {1}({2}, \"{3}\", \"{4}\", LookupDocumentation(\"{5}\"), \"{5}\", {6}, {7});",
                value.MemberName,
                value.SettingInstanceType,
                value.CategoryFullMemberName,
                value.UIText,
                value.DocumentationTitle,
                value.OscAddress,
                value.IsReadOnly.ToString().ToLowerInvariant(),
                value.IsHidden.ToString().ToLowerInvariant()));
        }

        private static void GenerateSettingMemberAttach(StringBuilder Members_Attach, SettingValue value)
        {
            Members_Attach.AppendLine(string.Format("{0} = settings.{0};", value.MemberName));
        }

        private static void GenerateSettingMember(StringBuilder Members, SettingValue value)
        {
            Members.AppendLine("/// <summary>");
            Members.AppendLine("/// Gets " + value.DocumentationTitle + " setting.");
            Members.AppendLine("/// </summary>");
            Members.AppendLine(string.Format("public ISettingValue<{0}> {1} {{ get; private set; }}", value.ValueType, value.MemberName));
            Members.AppendLine();
        }

        private static string PrefixLines(IEnumerable<string> blocks, char @char, int count)
        {
            StringBuilder sb = new StringBuilder();

            string prefix = new string(@char, count);

            foreach (string block in blocks)
            {
                string[] lines = block.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

                foreach (string line in lines)
                {
                    sb.AppendLine(prefix + line);
                }
            }

            return sb.ToString().TrimEnd();
        }
    }
}
