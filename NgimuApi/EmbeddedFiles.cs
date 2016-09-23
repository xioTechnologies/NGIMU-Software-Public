using System;
using System.IO;
using System.Reflection;

namespace NgimuApi
{
    static class EmbeddedFiles
    {
        public static string SettingsXml { get; private set; }

        static EmbeddedFiles()
        {
            SettingsXml = LoadXml("SettingDocumentation.xml", typeof(EmbeddedFiles));
        }

        public static string LoadXml(string path, Type type)
        {
            Assembly asm = type.Assembly;

            Stream file = asm.GetManifestResourceStream(type, path);
            using (StreamReader reader = new StreamReader(file))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
