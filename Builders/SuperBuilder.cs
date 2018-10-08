using System.Collections.Generic;
using Solidoc.Models;
using Solidoc.Utility;

namespace Solidoc.Builders
{
    public sealed class SuperBuilder
    {
        public SuperBuilder(Node node, IEnumerable<Contract> contracts)
        {
            this.Node = node;
            this.Contracts = contracts;
        }

        public Node Node { get; }
        public IEnumerable<Contract> Contracts { get; }

        public string Build()
        {
            int super = this.Node.SuperFunction ?? 0;
            var baseContract = new Contract();

            if (super == 0)
            {
                return string.Empty;
            }

            foreach (var contract in this.Contracts)
            {
                var result = contract.Ast.Nodes.FindNodeById(super);

                if (result.Id != super)
                {
                    continue;
                }

                baseContract = contract;
                break;
            }

            return "⤾ " + string.Format(I18N.OverridesSuperFunction, $"[{baseContract.ContractName}.{this.Node.Name}]({baseContract.ContractName}.md#{this.Node.Name.ToLower()})");
        }
    }
}