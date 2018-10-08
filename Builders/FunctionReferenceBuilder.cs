using System.Collections.Generic;
using System.Linq;
using Solidoc.Models;
using Solidoc.Utility;

namespace Solidoc.Builders
{
    public sealed class FunctionReferenceBuilder
    {
        public FunctionReferenceBuilder(Node node, IEnumerable<Contract> contracts)
        {
            this.Node = node;
            this.Contracts = contracts;
        }

        public Node Node { get; }
        public IEnumerable<Contract> Contracts { get; }

        public string Build()
        {
            int id = this.Node.Id.Value;
            var references = new List<string>();
            var implementations = this.Contracts.FindOverriddenNodesById(id);

            if (id == 0)
            {
                return string.Empty;
            }

            foreach (var implementation in implementations)
            {
                references.Add($"[{implementation.Contract.ContractName}.{implementation.Node.Name}]({implementation.Contract.ContractName}.md#{implementation.Node.Name.ToLower()})");
            }

            if(!references.Any())
            {
                return string.Empty;
            }

            //$"[{baseContract.ContractName}.{this.Node.Name}]({baseContract.ContractName}.md#{this.Node.Name.ToLower()})"
            return "⤿ " + string.Format(I18N.Overridden, string.Join(",", references));
        }
    }
}