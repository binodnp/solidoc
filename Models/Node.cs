using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Solidoc.Models
{
    public sealed class Node
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string CanonicalName { get; set; }
        public IEnumerable<string> Literals { get; set; }
        public string NodeType { get; set; }
        public string Src { get; set; }
        public string AbsolutePath { get; set; }
        public string File { get; set; }
        public int? Scope { get; set; }
        public int? SourceUnit { get; set; }
        public IEnumerable<object> SymbolAliases { get; set; }
        public string UnitAlias { get; set; }
        public IEnumerable<int> ContractDependencies { get; set; }

        public IEnumerable<Node> BaseContracts { get; set; }

        public IEnumerable<Node> Members { get; set; }

        public IEnumerable<TypeDescriptor> ArgumentTypes { get; set; }
        public IEnumerable<int> OverloadedDeclarations { get; set; }

        public Node BaseName { get; set; }
        public int? ContractScope { get; set; }
        public int? ReferencedDeclaration { get; set; }
        public TypeDescriptor TypeDescriptions { get; set; }
        public string ContractKind { get; set; }
        public string Documentation { get; set; }
        public IEnumerable<int> LinearizedBaseContracts { get; set; }
        public IEnumerable<Node> Nodes { get; set; }

        public Node Body { get; set; }

        public bool? Implemented { get; set; }
        public bool? FullyImplemented { get; set; }
        public bool? IsConstructor { get; set; }
        public bool? IsDeclaredConst { get; set; }
        public bool? Payable { get; set; }
        public bool? Indexed { get; set; }
        public string StateMutability { get; set; }
        public bool? Constant { get; set; }
        public bool? StateVariable { get; set; }
        public IEnumerable<dynamic> Statements { get; set; }

        public IEnumerable<Modifier> Modifiers { get; set; }

        public ParameterList Parameters { get; set; }
        public ParameterList ReturnParameters { get; set; }

        public string StorageLocation { get; set; }
        public Node TypeName { get; set; }

        public object Value { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Visibility? Visibility { get; set; }

        public int? SuperFunction { get; set; }
    }
}