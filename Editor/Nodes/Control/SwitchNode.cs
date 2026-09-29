using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Unity.GraphToolkit.Editor;
using UnityEditor.Experimental.GraphView;

namespace Editor.Nodes.Control
{
    [Serializable]
    [Node("Control", "", "Switch", StylePath)]
    public class SwitchNode : ComputeNodeWildcardBase
    {
        private const string k_NumCases = "NumCases";

        protected override void OnDefineOptions(IOptionDefinitionContext context)
        {
            context.AddOption<int>(k_NumCases)
                .WithDisplayName("Num Cases")
                .WithDefaultValue(2);
        }
        
        protected override IEnumerable<PortDefinition> DefinedPorts
        {
            get 
            {
                GetNodeOptionByName(k_NumCases).TryGetValue<int>(out var numCases);

                IEnumerable<PortDefinition> portDefinitions = new List<PortDefinition>();

                portDefinitions = portDefinitions.Append(PortDefinition.Input("In", typeof(int)));
                portDefinitions = portDefinitions.Append(PortDefinition.Output("Out", isWildcard: true));
                
                for (int i = 0; i < numCases; i++)
                {
                    portDefinitions = portDefinitions.Append(PortDefinition.Input($"{i}", isWildcard: true));
                }
                
                portDefinitions = portDefinitions.Append(PortDefinition.Input("Default", isWildcard: true));
                
                return portDefinitions;
            }
        }
        
        protected override void EmitHLSL(ComputeGraphCompiler compiler, string outputVar)
        {
            // Get number of cases
            GetNodeOptionByName(k_NumCases).TryGetValue<int>(out var numCases);
            
            // Input
            string index = EvaluateInput(compiler, "In");
            var valueType = ComputeGraphTypes.GetStringFromCSType(ResolvedType);
            
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