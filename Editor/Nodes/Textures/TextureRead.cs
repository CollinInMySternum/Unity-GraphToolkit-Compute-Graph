using System;
using Unity.GraphToolkit.Editor;
using Unity.Mathematics;
using UnityEngine;

namespace Editor.Nodes.Textures
{
    [Serializable]
    [Node("Textures/Texture Read", "", "Texture Read", StylePath)]
    public class TextureReadNode : ComputeNodeWildcardBase
    {
        public override string[] wildcardPorts => new[] { "Texture" };

        public override bool IsValidWildcardType(Type type)
        {
            return ComputeGraphTypes.IsTexture(type);
        }

        protected override void OnDefinePorts(IPortDefinitionContext context)
        {
            context.AddInputPort("Texture")
                .WithDataType(resolvedType)
                .WithConnectorUI(PortConnectorUI.Arrowhead)
                .Build();
            
            context.AddInputPort("Indices")
                .WithDataType(
                    resolvedType == typeof(Texture2D) ? typeof(int2) :
                    resolvedType == typeof(Texture3D) ? typeof(int3) :
                    resolvedType == typeof(ComputeGraphTypes.RWTexture2D) ? typeof(int2) :
                    resolvedType == typeof(ComputeGraphTypes.RWTexture3D) ? typeof(int3) :
                        typeof(Untyped)
                )
                .Build();
            
            context.AddOutputPort<float4>("Out (RGBA)").Build();
        }

        protected override void EmitHLSL(ComputeGraphCompiler compiler, string outputVar)
        {
            string texture = EvaluateInput(compiler, "Texture");
            string indices = EvaluateInput(compiler, "Indices");

            compiler.Body.AppendLine($"    float4 {outputVar} = {texture}[{indices}].rgba;");
        }
    }
}