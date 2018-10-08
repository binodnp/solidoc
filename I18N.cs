using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace Solidoc
{
    public static class I18N
    {
        static I18N()
        {
            string contents = ResourceWriter.GetContents();
            var action = new DeserializerBuilder().Build();
            Resource = action.Deserialize<IDictionary<string, string>>(contents);
        }

        public static IDictionary<string, string> Resource { get; }

        /// <summary>
        /// Returns localized string for "Enums"
        /// </summary>
        public static string Enums => Resource["Enums"];

        /// <summary>
        /// Returns localized string for "Functions"
        /// </summary>
        public static string Functions => Resource["Functions"];

        /// <summary>
        /// Returns localized string for "Arguments"
        /// </summary>
        public static string Arguments => Resource["Arguments"];

        /// <summary>
        /// Returns localized string for "Modifiers"
        /// </summary>
        public static string Modifiers => Resource["Modifiers"];

        /// <summary>
        /// Returns localized string for "Structs"
        /// </summary>
        public static string Structs => Resource["Structs"];

        /// <summary>
        /// Returns localized string for "Constructor"
        /// </summary>
        public static string Constructor => Resource["Constructor"];

        /// <summary>
        /// Returns localized string for "Returns"
        /// </summary>
        public static string Returns => Resource["Returns"];

        /// <summary>
        /// Returns localized string for "Contract Members"
        /// </summary>
        public static string ContractMembers => Resource["ContractMembers"];

        /// <summary>
        /// Returns localized string for "Constants & Variables"
        /// </summary>
        public static string ConstantsAndVariables => Resource["ConstantsAndVariables"];

        /// <summary>
        /// Returns localized string for "//{0} members"
        /// </summary>
        public static string VisibilityMembers => Resource["VisibilityMembers"];

        /// <summary>
        /// Returns localized string for "overrides {0}"
        /// </summary>
        public static string OverridesSuperFunction => Resource["OverridesSuperFunction"];

        /// <summary>
        /// Returns localized string for "Invalid command ->  {0}"
        /// </summary>
        public static string InvalidCommand => Resource["InvalidCommand"];

        /// <summary>
        /// Returns localized string for "Invalid directory ->  {0}"
        /// </summary>
        public static string InvalidDirectory => Resource["InvalidDirectory"];

        /// <summary>
        /// Returns localized string for "Writing {0}"
        /// </summary>
        public static string WritingPath => Resource["WritingPath"];

        /// <summary>
        /// Returns localized string for "Events"
        /// </summary>
        public static string Events => Resource["Events"];

        /// <summary>
        /// Returns localized string for "↘ Derived Contracts: {0}"
        /// </summary>
        public static string DerivedContracts => Resource["DerivedContracts"];

        /// <summary>
        /// Returns localized string for "References: {0}"
        /// </summary>
        public static string References => Resource["References"];

        /// <summary>
        /// Returns localized string for "Overridden Implementation(s): {0}"
        /// </summary>
        public static string Overridden => Resource["Overridden"];

        /// <summary>
        /// Returns localized string for "↗ Extends: {0}"
        /// </summary>
        public static string Extends => Resource["Extends"];

        /// <summary>
        /// Returns localized string for "Parsing: {0}"
        /// </summary>
        public static string ParsingPath => Resource["ParsingPath"];
    }
}