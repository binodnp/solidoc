using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Solidoc.Utility;
using YamlDotNet.Serialization;

namespace Solidoc
{
    public static class ResourceWriter
    {
        public static string GetContents()
        {
            string langauge = ConfigurationManager.Get("Language");

            if (string.IsNullOrWhiteSpace(langauge))
            {
                langauge = "en";
            }

            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            path = Path.Combine(path, "i18n", $"{langauge}.yaml");

            string contents = File.ReadAllText(path, new UTF8Encoding(false));
            return contents;
        }

        private static string GetTemplate()
        {
            return ConfigurationManager.Get("I18NTemplate");
        }

        public static void Run()
        {
            string contents = GetContents();
            var action = new DeserializerBuilder().Build();
            var result = action.Deserialize<IDictionary<string, string>>(contents);
            var resources = new List<string>();

            string template = GetTemplate();

            foreach (var item in result)
            {
                string resource = $"\t\t/// <summary>{Environment.NewLine}\t\t/// Returns localized string for \"{item.Value}\"{Environment.NewLine}\t\t/// </summary>{Environment.NewLine}\t\tpublic static string {item.Key} => Resource[\"{item.Key}\"];";
                resources.Add(resource.Replace("\t", string.Join("", Enumerable.Repeat(" ", 4))));
            }

            if (!resources.Any())
            {
                return;
            }

            template = template.Replace("{{Resources}}", string.Join(Environment.NewLine.Copy(2), resources));
            var directory = new DirectoryInfo(Assembly.GetExecutingAssembly().Location);
            string path = Path.Combine(directory.Parent?.Parent?.Parent?.Parent?.FullName, "I18N.cs");

            File.WriteAllText(path, template, new UTF8Encoding(false));
        }
    }
}