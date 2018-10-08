using System;
using System.Collections.Generic;
using System.Linq;
using Solidoc.Builders;
using Solidoc.Models;
using Solidoc.Utility;

namespace Solidoc.Serializers
{
    public sealed class FunctionSerializer : ISerializer
    {
        public string Serialize(Contract contract, string template, List<Contract> contracts)
        {
            var functionNodes = NodeHelper.GetFunctions(contract).Where(x => !x.IsConstructor.HasValue || !x.IsConstructor.Value).ToList();

            if (!functionNodes.Any())
            {
                return Clean(template);
            }

            var definitionList = new List<string>();
            var functionList = functionNodes.Select(node => $"- [{node.Name}](#{node.Name.ToLower()})").ToList();


            template = template.Replace("{{FunctionTitle}}", $"## {I18N.Functions}");

            foreach (var node in functionNodes)
            {
                string functionTemplate = TemplateHelper.Function;
                string documentation = node.Documentation;
                string description = DocumentationHelper.GetNotice(documentation);
                var codeBuilder = new FunctionCodeBuilder(node);
                var superBuilder = new SuperBuilder(node, contracts);
                var referenceBuilder = new FunctionReferenceBuilder(node, contracts);

                functionTemplate = functionTemplate.Replace("{{FunctionName}}", node.Name);
                functionTemplate = functionTemplate.Replace("{{FQFunctionName}}", $"{contract.ContractName}.{node.Name}");
                functionTemplate = functionTemplate.Replace("{{FunctionNameHeading}}", $"### {node.Name}");
                functionTemplate = functionTemplate.Replace("{{Super}}", superBuilder.Build());
                functionTemplate = functionTemplate.Replace("{{References}}", referenceBuilder.Build());
                functionTemplate = functionTemplate.Replace("{{FunctionDescription}}", description);
                functionTemplate = functionTemplate.Replace("{{FunctionCode}}", codeBuilder.Build());

                var parameters = node.Parameters.Parameters.ToList();
                var argumentBuilder = new ArgumentBuilder(node.Documentation, parameters);

                functionTemplate = functionTemplate.Replace("{{TableHeader}}", parameters.Any() ? TemplateHelper.TableHeader : string.Empty);
                functionTemplate = functionTemplate.Replace("{{FunctionArgumentsHeading}}", parameters.Any() ? $"**{I18N.Arguments}**" : string.Empty);
                functionTemplate = functionTemplate.Replace("{{FunctionArguments}}", argumentBuilder.Build());

                definitionList.Add(functionTemplate);
            }

            template = template.Replace("{{FunctionList}}", string.Join(Environment.NewLine, functionList));
            template = template.Replace("{{AllFunctions}}", string.Join(Environment.NewLine, definitionList));

            return template;
        }


        private static string Clean(string template)
        {
            template = template.Replace("{{FunctionTitle}}", string.Empty);
            template = template.Replace("{{FunctionList}}", string.Empty);
            template = template.Replace("{{AllFunctions}}", string.Empty);

            return template;
        }
    }
}