using System;
using System.Collections.Generic;
using Unity.GraphToolkit.Editor;
using Unity.Mathematics;
using UnityEngine;

namespace Editor.Nodes.Vector
{
    [Serializable]
    [Node("Math/Float2", "", "Make Float2", StylePath)]
    public class MakeFloat2 : ComputeNodeBase
    {
        protected override IEnumerable<PortDefinition> DefinedPorts => new[]
        {
            PortDefinition.Input("X", type: typeof(float)),
            PortDefinition.Input("Y", type: typeof(float)),
            
            PortDefinition.Output("Out", type: typeof(float2)),
        };

        protected override void EmitHLSL(ComputeGraphCompiler compiler, string outputVar)
        {
            string x = EvaluateInput(compiler, "X");
            string y = EvaluateInput(compiler, "Y");
                
            compiler.Body.AppendLine($"    float2 {outputVar} = float2({x}, {y});");
        }
    }
    
    [Serializable]
    [Node("Math/Float3", "", "Make Float3", StylePath)]
    public class MakeFloat3 : ComputeNodeBase
    {
        protected override IEnumerable<PortDefinition> DefinedPorts => new[]
        {
            PortDefinition.Input("X", type: typeof(float)),
            PortDefinition.Input("Y", type: typeof(float)),
            PortDefinition.Input("Z", type: typeof(float)),
            
            PortDefinition.Output("Out", type: typeof(float3)),
        };

        protected override void EmitHLSL(ComputeGraphCompiler compiler, string outputVar)
        {
            string x = EvaluateInput(compiler, "X");
            string y = EvaluateInput(compiler, "Y");
            string z = EvaluateInput(compiler, "Z");
                
            compiler.Body.AppendLine($"    float3 {outputVar} = float3({x}, {y}, {z});");
        }
    }
    
    [Serializable]
    [Node("Math/Float4", "", "Make Float4", StylePath)]
    public class MakeFloat4 : ComputeNodeBase
    {
        protected override IEnumerable<PortDefinition> DefinedPorts => new[]
        {
            PortDefinition.Input("X", type: typeof(float)),
            PortDefinition.Input("Y", type: typeof(float)),
            PortDefinition.Input("Z", type: typeof(float)),
            PortDefinition.Input("W", type: typeof(float)),
            
            PortDefinition.Output("Out", type: typeof(float4)),
        };

        protected override void EmitHLSL(ComputeGraphCompiler compiler, string outputVar)
        {
            string x = EvaluateInput(compiler, "X");
            string y = EvaluateInput(compiler, "Y");
            string z = EvaluateInput(compiler, "Z");
            string w = EvaluateInput(compiler, "W");
                
            compiler.Body.AppendLine($"    float4 {outputVar} = float4({x}, {y}, {z}, {w});");
        }
    }
    
    [Serializable]
    [Node("Math/Int2", "", "Make Int2", StylePath)]
    public class MakeInt2 : ComputeNodeBase
    {
        protected override IEnumerable<PortDefinition> DefinedPorts => new[]
        {
            PortDefinition.Input("X", type: typeof(int)),
            PortDefinition.Input("Y", type: typeof(int)),
            
            PortDefinition.Output("Out", type: typeof(int2)),
        };

        protected override void EmitHLSL(ComputeGraphCompiler compiler, string outputVar)
        {
            string x = EvaluateInput(compiler, "X");
            string y = EvaluateInput(compiler, "Y");
                
            compiler.Body.AppendLine($"    int2 {outputVar} = int2({x}, {y});");
        }
    }
    
    [Serializable]
    [Node("Math/Int3", "", "Make Int3", StylePath)]
    public class MakeInt3 : ComputeNodeBase
    {
        protected override IEnumerable<PortDefinition> DefinedPorts => new[]
        {
            PortDefinition.Input("X", type: typeof(int)),
            PortDefinition.Input("Y", type: typeof(int)),
            PortDefinition.Input("Z", type: typeof(int)),
            
            PortDefinition.Output("Out", type: typeof(int3)),
        };

        protected override void EmitHLSL(ComputeGraphCompiler compiler, string outputVar)
        {
            string x = EvaluateInput(compiler, "X");
            string y = EvaluateInput(compiler, "Y");
            string z = EvaluateInput(compiler, "Z");
                
            compiler.Body.AppendLine($"    int3 {outputVar} = int3({x}, {y}, {z});");
        }
    }
    
    [Serializable]
    [Node("Math/Int4", "", "Make Int4", StylePath)]
    public class MakeInt4 : ComputeNodeBase
    {
        protected override IEnumerable<PortDefinition> DefinedPorts => new[]
        {
            PortDefinition.Input("X", type: typeof(int)),
            PortDefinition.Input("Y", type: typeof(int)),
            PortDefinition.Input("Z", type: typeof(int)),
            PortDefinition.Input("W", type: typeof(int)),
            
            PortDefinition.Output("Out", type: typeof(int4)),
        };

        protected override void EmitHLSL(ComputeGraphCompiler compiler, string outputVar)
        {
            string x = EvaluateInput(compiler, "X");
            string y = EvaluateInput(compiler, "Y");
            string z = EvaluateInput(compiler, "Z");
            string w = EvaluateInput(compiler, "W");
                
            compiler.Body.AppendLine($"    int4 {outputVar} = int4({x}, {y}, {z}, {w});");
        }
    }
}