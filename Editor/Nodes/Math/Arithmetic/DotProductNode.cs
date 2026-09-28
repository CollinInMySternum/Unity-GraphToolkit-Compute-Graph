using System;
using System.Collections.Generic;
using Unity.GraphToolkit.Editor;
using Unity.Mathematics;

namespace Editor.Nodes.Math.Arithmetic
{
    [Serializable]
    [Node("Math/Arithmetic", "", "Dot Product", StylePath)]
    public class DotProductNode : ComputeNodeWildcardBase
    {
        protected override IEnumerable<PortDefinition> DefinedPorts => new[]
        {
            PortDefinition.Input("v1", isWildcard: true),
            PortDefinition.Input("v2", isWildcard: true),

            PortDefinition.Output("Out", type: typeof(float))
        };

        protected override void EmitHLSL(ComputeGraphCompiler compiler, string outputVar)
        {
            string valv1 = EvaluateInput(compiler, "v1");
            string valv2 = EvaluateInput(compiler, "v2");

            compiler.Body.AppendLine($"    float {outputVar} = dot({valv1}, {valv2});");
        }
    }
}