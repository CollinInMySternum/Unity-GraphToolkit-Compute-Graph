using System;
using Unity.GraphToolkit.Editor;
using Unity.Mathematics;
using UnityEngine;

namespace Editor.Nodes.Textures
{
    [Serializable]
    [Node("Textures/Texture Read", "", "Texture Read", StylePath)]
    public class TextureReadNode : ComputeNodeBase
    {
        protected override void OnDefinePorts(IPortDefinitionContext context)
        {
            context.AddInputPort<Texture2D>("Texture Object").Build();
            context.AddInputPort<int2>("Indices").Build();

            context.AddOutputPort<float4>("Out (RGBA)").Build();
        }

        protected override void EmitHLSL(ComputeGraphCompiler compiler, string outputVar)
        {
            string texture = EvaluateInput(compiler, "Texture Object");
            string indices = EvaluateInput(compiler, "Indices");

            compiler.Body.AppendLine($"    float4 {outputVar} = {texture}[{indices}].rgba;");
        }
    }
}