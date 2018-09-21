﻿using System.Collections.Generic;
using System.Linq;
using Solidoc.Models;

namespace Solidoc.Utility
{
    public static class NodeHelper
    {
        public static Node FindNodeById(this Node node, int id)
        {
            if (node.Id == id)
            {
                return node;
            }

            if (node.Nodes != null)
            {
                foreach (var child in node.Nodes)
                {
                    return child.Id == id ? child : child.FindNodeById(id);
                }
            }

            if (node.Body?.Nodes == null)
            {
                return new Node();
            }

            foreach (var child in node.Body.Nodes)
            {
                return child.Id == id ? child : child.FindNodeById(id);
            }

            return new Node();
        }

        public static Node FindNodeById(this IEnumerable<Node> nodes, int id)
        {
            foreach (var node in nodes)
            {
                if (node.Id.HasValue && node.Id.Value == id)
                {
                    return node;
                }

                var found = node.FindNodeById(id);

                if (found.Id.HasValue && found.Id.Value == id)
                {
                    return found;
                }
            }

            return new Node();
        }

        public static Node FindNodeById(this Contract contract, int id)
        {
            return contract.Ast.Nodes.FindNodeById(id);
        }

        public static Node FindNodeById(this IEnumerable<Contract> contracts, int id)
        {
            foreach (var contract in contracts)
            {
                var node = contract.FindNodeById(id);

                if (node.Id.HasValue && node.Id.Value == id)
                {
                    return node;
                }
            }

            return new Node();
        }

        public static IEnumerable<Node> GetBaseContracts(Contract contract)
        {
            var node = contract.Ast.Nodes.FirstOrDefault(x => x.BaseContracts != null && x.BaseContracts.Any());

            if (node != null)
            {
                foreach (var baseContract in node.BaseContracts)
                {
                    yield return baseContract;
                }
            }
        }

        public static Node GetConstructorNode(Contract contract)
        {
            var constructorNode = contract.Ast.Nodes.FirstOrDefault(x => x.IsConstructor.HasValue && x.IsConstructor.Value);

            if (constructorNode != null)
            {
                return constructorNode;
            }

            foreach (var node in contract.Ast.Nodes.Where(x => x.Nodes != null))
            {
                constructorNode = node.Nodes.FirstOrDefault(x => x.IsConstructor.HasValue && x.IsConstructor.Value);
            }

            return constructorNode ?? new Node();
        }

        public static Node GetContractNode(Contract contract)
        {
            var contractNode = contract.Ast.Nodes.FirstOrDefault(x => x.ContractKind == "contract");

            if (contractNode != null)
            {
                return contractNode;
            }

            foreach (var node in contract.Ast.Nodes.Where(x => x.Nodes != null))
            {
                contractNode = node.Nodes.FirstOrDefault(x => x.ContractKind == "contract");
            }

            return contractNode ?? new Node();
        }

        public static IEnumerable<Node> GetReturnParameters(Node node)
        {
            return node.ReturnParameters.Parameters;
        }

        public static IEnumerable<Node> GetEnumerators(Contract contract)
        {
            var contractNode = contract.Ast.Nodes.FirstOrDefault(x => x.NodeType == "EnumDefinition");

            if (contractNode != null)
            {
                yield return contractNode;
            }

            foreach (var node in contract.Ast.Nodes.Where(x => x.Nodes != null))
            {
                if (node.NodeType == "EnumDefinition")
                {
                    yield return node;
                }

                foreach (var modifierNode in node.Nodes.Where(x => x.NodeType == "EnumDefinition"))
                {
                    yield return modifierNode;
                }
            }
        }

        public static IEnumerable<Node> GetMembers(Contract contract)
        {
            var contracts = contract.Ast.Nodes.Where(x => x.NodeType == "ContractDefinition").ToList();

            foreach (var node in contracts)
            {
                foreach (var member in node.Nodes)
                {
                    if (member.NodeType == "VariableDeclaration")
                    {
                        yield return member;
                    }
                }
            }
        }

        public static IEnumerable<Node> GetStructs(Contract contract)
        {
            var contractNode = contract.Ast.Nodes.FirstOrDefault(x => x.NodeType == "StructDefinition");

            if (contractNode != null)
            {
                yield return contractNode;
            }

            foreach (var node in contract.Ast.Nodes.Where(x => x.Nodes != null))
            {
                if (node.NodeType == "StructDefinition")
                {
                    yield return node;
                }

                foreach (var modifierNode in node.Nodes.Where(x => x.NodeType == "StructDefinition"))
                {
                    yield return modifierNode;
                }
            }
        }

        public static IEnumerable<Node> GetModifiers(Contract contract)
        {
            var contractNode = contract.Ast.Nodes.FirstOrDefault(x => x.NodeType == "ModifierDefinition");

            if (contractNode != null)
            {
                yield return contractNode;
            }

            foreach (var node in contract.Ast.Nodes.Where(x => x.Nodes != null))
            {
                if (node.NodeType == "ModifierDefinition")
                {
                    yield return node;
                }

                foreach (var modifierNode in node.Nodes.Where(x => x.NodeType == "ModifierDefinition"))
                {
                    yield return modifierNode;
                }
            }
        }


        public static IEnumerable<Node> GetEvents(Contract contract)
        {
            var contractNode = contract.Ast.Nodes.FirstOrDefault(x => x.NodeType == "EventDefinition");

            if (contractNode != null)
            {
                yield return contractNode;
            }

            foreach (var node in contract.Ast.Nodes.Where(x => x.Nodes != null))
            {
                if (node.NodeType == "EventDefinition")
                {
                    yield return node;
                }

                foreach (var modifierNode in node.Nodes.Where(x => x.NodeType == "EventDefinition"))
                {
                    yield return modifierNode;
                }
            }
        }

        public static IEnumerable<Node> GetFunctions(Contract contract)
        {
            var contractNode = contract.Ast.Nodes.FirstOrDefault(x => x.NodeType == "FunctionDefinition");

            if (contractNode != null)
            {
                yield return contractNode;
            }

            foreach (var node in contract.Ast.Nodes.Where(x => x.Nodes != null))
            {
                if (node.NodeType == "FunctionDefinition")
                {
                    yield return node;
                }

                foreach (var modifierNode in node.Nodes.Where(x => x.NodeType == "FunctionDefinition"))
                {
                    yield return modifierNode;
                }
            }
        }
    }
}