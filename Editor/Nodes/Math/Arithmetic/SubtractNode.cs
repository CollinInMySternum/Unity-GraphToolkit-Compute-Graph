using System;
using System.Collections.Generic;
using Unity.GraphToolkit.Editor;
using Unity.Mathematics;
using UnityEngine;

namespace Editor.Nodes.Math.Arithmetic
{
    [Serializable]
    [Node("Math/Arithmetic", "", "Subtract", StylePath)]
    public class SubtractNode : ComputeNodeWildcardBase
    {
        protected override IEnumerable<PortDefinition> DefinedPorts => new[]
        {
            PortDefinition.Input("A", isWildcard: true),
            PortDefinition.Input("B", isWildcard: true),

            PortDefinition.Output("Out", isWildcard: true)
        };

        protected override void EmitHLSL(ComputeGraphCompiler compiler, string outputVar)
        {
            string valA = EvaluateInput(compiler, "A");
            string valB = EvaluateInput(compiler, "B");

            string hlslType = ComputeGraphTypes.GetStringFromCSType(ResolvedType);

            compiler.Body.AppendLine($"    {hlslType} {outputVar} = {valA} - {valB};");
        }
    }
}