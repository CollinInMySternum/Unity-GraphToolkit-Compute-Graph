using System;
using System.Collections.Generic;
using Unity.GraphToolkit.Editor;
using Unity.Mathematics;

namespace Editor.Nodes.Math.Vector
{
    [Serializable]
    [Node("Math/Vector", "", "Dot Product", StylePath)]
    public class DotProductNode : ComputeNodeWildcardBase
    {
        public override bool IsValidWildcardType(Type type)
        {
            return type == typeof(float2) ||
                   type == typeof(float3) ||
                   type == typeof(float4) ||
                   type == typeof(int2) ||
                   type == typeof(int3) ||
                   type == typeof(int4);
        }

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