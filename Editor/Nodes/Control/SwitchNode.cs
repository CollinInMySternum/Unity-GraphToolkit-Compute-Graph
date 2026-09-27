using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Unity.GraphToolkit.Editor;

namespace Editor.Nodes.Control
{
    [Serializable]
    [Node("Control", "", "Switch", StylePath)]
    public class SwitchNode : ComputeNodeWildcardBase
    {
        private const string k_NumCases = "NumCases";
        
        public override string[] wildcardPorts
        {
            // Define wildcard ports dynamically from NumCases
            get
            {
                var ports = new List<string>{"Out"};

                // Cases
                if (GetNodeOptionByName(k_NumCases).TryGetValue<int>(out var numCases))
                {
                    for (int i = 0; i < numCases; i++)
                    {
                        ports.Add($"{i}");
                    }
                }
                
                // Default case
                ports.Add("Default");

                return ports.ToArray();
            }
        }

        protected override void OnDefineOptions(IOptionDefinitionContext context)
        {
            context.AddOption<int>(k_NumCases)
                .WithDisplayName("Num Cases")
                .WithDefaultValue(2);
        }

        protected override void OnDefinePorts(IPortDefinitionContext context)
        {
            GetNodeOptionByName(k_NumCases).TryGetValue<int>(out var numCases);
            
            // In/out
            context.AddInputPort<int>("In").Build();
            context.AddOutputPort("Out").WithDataType(resolvedType).Build();
            
            // Base cases
            for (int i = 0; i < numCases; i++)
            {
                context.AddInputPort($"{i}").WithDataType(resolvedType).Build();
            }
            
            // Default case
            context.AddInputPort("Default").WithDataType(resolvedType).Build();
        }

        protected override void EmitHLSL(ComputeGraphCompiler compiler, string outputVar)
        {
            // Get number of cases
            GetNodeOptionByName(k_NumCases).TryGetValue<int>(out var numCases);
            
            // Input
            string index = EvaluateInput(compiler, "In");
            var valueType = ComputeGraphTypes.GetStringFromCSType(resolvedType);
            
            // Evaluate and construct case lines
            string[] caseLines = new string[numCases + 1];

            for (int i = 0; i < numCases; i++)
            {
                string caseInput = EvaluateInput(compiler, $"{i}");
                caseLines[i] = $"        {index} == {i} ? {caseInput} : ";
            }
            
            // Handle default case
            string defaultInput = EvaluateInput(compiler, "Default");
            caseLines[numCases] = $"        {defaultInput};";
            
            // Base definition line
            compiler.Body.AppendLine($"    {valueType} {outputVar} = ");
            
            // Submit case lines
            foreach (var caseLine in caseLines)
            {
                compiler.Body.AppendLine(caseLine);
            }
        }
    }
}