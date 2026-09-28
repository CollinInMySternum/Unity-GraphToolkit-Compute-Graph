using System;
using Unity.GraphToolkit.Editor;
using Unity.Mathematics;

namespace Editor.Nodes.Math.Arithmetic
{
    [Serializable]
    [Node("Math/Arithmetic", "", "Dot Product", StylePath)]
    public class DotProductNode : ComputeNodeWildcardBase
    {
        public override string[] wildcardPorts => new[] { "v1", "v2" };

        public override bool IsValidWildcardType(Type type)
        {
            // Only support float vectors
            return (type == typeof(float2)) || (type == typeof(float3)) || (type == typeof(float4));
        }

        protected override void OnDefinePorts(IPortDefinitionContext context)
        {
            context.AddInputPort("v1")
                .WithDataType(resolvedType)
                .Build();
            
            context.AddInputPort("v2")
                .WithDataType(resolvedType)
                .Build();

            context.AddOutputPort<float>("Out");
        }

        protected override void EmitHLSL(ComputeGraphCompiler compiler, string outputVar)
        {
            string valv1 = EvaluateInput(compiler, "v1");
            string valv2 = EvaluateInput(compiler, "v2");

            compiler.Body.AppendLine($"    float {outputVar} = dot({valv1}, {valv2});");
        }
    }
}