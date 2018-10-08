using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Solidoc.Models;
using Solidoc.Utility;

namespace Solidoc
{
    public sealed class ContractParser
    {
        public ContractParser(string buildDirectory)
        {
            this.BuildDirectory = buildDirectory;
        }

        private string BuildDirectory { get; }

        public IEnumerable<Contract> Parse()
        {
            var directory = new DirectoryInfo(this.BuildDirectory);

            if (!directory.Exists)
            {
                ConsoleUtility.WriteException(string.Format(I18N.InvalidDirectory,  string.Join(" ", directory.FullName)));
                yield return null;
            }

            var files = directory.GetFiles("*.json");

            foreach (var file in files)
            {
                var contract = Parse(file.FullName);
                yield return contract;
            }
        }

        private static Contract Parse(string path)
        {
            Console.WriteLine(I18N.ParsingPath, path);
            
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            string contents = File.ReadAllText(path, new UTF8Encoding(false));
            var parsed = JsonConvert.DeserializeObject<Contract>(contents, settings);

            return parsed;
        }
    }
}