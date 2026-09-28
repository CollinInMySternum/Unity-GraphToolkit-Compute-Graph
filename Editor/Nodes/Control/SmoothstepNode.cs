using System;
using System.Collections.Generic;
using Unity.GraphToolkit.Editor;

namespace Editor.Nodes.Control
{
    [Serializable]
    [Node("Control", "", "Smoothstep")]
    public class SmoothstepNode : ComputeNodeWildcardBase
    {
        protected override IEnumerable<PortDefinition> DefinedPorts => new[]
        {
            PortDefinition.Input("A", isWildcard: true),
            PortDefinition.Input("B", isWildcard: true),
            PortDefinition.Input("X", typeof(float)),
            
            PortDefinition.Output("Out", isWildcard: true)
        };

        protected override void EmitHLSL(ComputeGraphCompiler compiler, string outputVar)
        {
            string valA = EvaluateInput(compiler, "A");
            string valB = EvaluateInput(compiler, "B");
            string valX = EvaluateInput(compiler, "X");

            string hlslType = ComputeGraphTypes.GetStringFromCSType(ResolvedType);
            
            compiler.Body.AppendLine($"    {hlslType} {outputVar} = smoothstep({valA}, {valB}, {valX});");
        }
    }
}