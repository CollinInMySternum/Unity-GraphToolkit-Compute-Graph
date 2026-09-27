using System;
using Unity.GraphToolkit.Editor;
using Unity.Mathematics;
using UnityEngine;

namespace Editor.Nodes.Vector
{
    [Serializable]
    [Node("Math/Float2", "", "Make Float2", StylePath)]
    public class MakeFloat2 : ComputeNodeBase
    {
        protected override void OnDefinePorts(IPortDefinitionContext context)
        {
            context.AddInputPort<float>("X").Build();
            context.AddInputPort<float>("Y").Build();
            
            context.AddOutputPort<float2>("Out").Build();
        }

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
        protected override void OnDefinePorts(IPortDefinitionContext context)
        {
            context.AddInputPort<float>("X").Build();
            context.AddInputPort<float>("Y").Build();
            context.AddInputPort<float>("Z").Build();
            
            context.AddOutputPort<float3>("Out").Build();
        }

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
        protected override void OnDefinePorts(IPortDefinitionContext context)
        {
            context.AddInputPort<float>("X").Build();
            context.AddInputPort<float>("Y").Build();
            context.AddInputPort<float>("Z").Build();
            context.AddInputPort<float>("W").Build();
            
            context.AddOutputPort<float4>("Out").Build();
        }

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
        protected override void OnDefinePorts(IPortDefinitionContext context)
        {
            context.AddInputPort<int>("X").Build();
            context.AddInputPort<int>("Y").Build();
            
            context.AddOutputPort<int2>("Out").Build();
        }

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
        protected override void OnDefinePorts(IPortDefinitionContext context)
        {
            context.AddInputPort<int>("X").Build();
            context.AddInputPort<int>("Y").Build();
            context.AddInputPort<int>("Z").Build();
            
            context.AddOutputPort<int3>("Out").Build();
        }

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
        protected override void OnDefinePorts(IPortDefinitionContext context)
        {
            context.AddInputPort<int>("X").Build();
            context.AddInputPort<int>("Y").Build();
            context.AddInputPort<int>("Z").Build();
            context.AddInputPort<int>("W").Build();
            
            context.AddOutputPort<int4>("Out").Build();
        }

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