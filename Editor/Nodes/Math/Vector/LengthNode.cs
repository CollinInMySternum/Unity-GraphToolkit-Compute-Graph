using System;
using System.Collections.Generic;
using Unity.GraphToolkit.Editor;
using Unity.Mathematics;

namespace Editor.Nodes.Math.Vector
{
    [Serializable]
    [Node("Math/Vector", "", "Length", StylePath)]
    public class LengthNode : ComputeNodeWildcardBase
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
        
        protected override IEnumerable<PortDefinition> DefinedPorts => new []
        {
            PortDefinition.Input("Vector", isWildcard: true),
            
            PortDefinition.Output("Out", type: typeof(float)), 
        };

        protected override void EmitHLSL(ComputeGraphCompiler compiler, string outputVar)
        {
            string valVector = EvaluateInput(compiler, "Vector");

            compiler.Body.AppendLine($"    float {outputVar} = length({valVector});");
        }
    }
}