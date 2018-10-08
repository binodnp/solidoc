using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Solidoc.Models;

namespace Solidoc.Builders
{
    public sealed class MemberBuilder
    {
        public MemberBuilder(List<Node> nodes)
        {
            this.Nodes = nodes;
        }

        public List<Node> Nodes { get; }


        public string Build()
        {
            var builder = new StringBuilder();

            builder.Append($"## {I18N.ContractMembers}");
            builder.Append(Environment.NewLine);
            builder.Append($"**{I18N.ConstantsAndVariables}**");
            builder.Append(Environment.NewLine);
            builder.Append(Environment.NewLine);
            builder.Append("```js");
            builder.Append(Environment.NewLine);

            var groups = this.Nodes.GroupBy(x => x.Visibility.ToString().ToLower()).ToList();

            foreach (var group in groups)
            {
                if (groups.Count > 1)
                {
                    builder.Append(string.Format(I18N.VisibilityMembers, group.Key));
                    builder.Append(Environment.NewLine);
                }

                foreach (var node in group)
                {
                    string constant = " ";

                    if (node.Constant != null && node.Constant.Value)
                    {
                        constant = " constant ";
                    }

                    builder.Append($"{node.TypeDescriptions.TypeString} {node.Visibility.ToString().ToLower()}{constant}{node.Name}");


                    builder.Append(";");

                    builder.Append(Environment.NewLine);
                }
                
                if(!groups.LastOrDefault().Equals(group))
                {
                    builder.Append(Environment.NewLine);
                }
            }


            builder.Append("```");
            builder.Append(Environment.NewLine);
            builder.Append(Environment.NewLine);

            return builder.ToString();
        }
    }
}