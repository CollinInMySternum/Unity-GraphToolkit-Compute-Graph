using System;
using Unity.GraphToolkit.Editor;
using Unity.Mathematics;
using UnityEngine;

namespace Editor.Nodes.Textures
{
    [Serializable]
    [Node("Textures", "", "Texture Write", StylePath)]
    public class TextureWriteNode : ComputeNodeWildcardBase, IComputeNodeOutput
    {
        public override string[] wildcardPorts => new[] { "RWTexture" };

        public override bool IsValidWildcardType(Type type)
        {
            return ComputeGraphTypes.IsTexture(type) && ComputeGraphTypes.IsReadWrite(type);
        }

        protected override void OnDefinePorts(IPortDefinitionContext context)
        {
            context.AddInputPort("RWTexture")
                .WithDataType(resolvedType)
                .WithConnectorUI(PortConnectorUI.Arrowhead)
                .Build();
            
            context.AddInputPort("Indices")
                .WithDataType(
                        resolvedType == typeof(ComputeGraphTypes.RWTexture2D) ? typeof(int2) :
                        resolvedType == typeof(ComputeGraphTypes.RWTexture3D) ? typeof(int3) :
                            typeof(Untyped)
                    )
                .Build();
            
            context.AddInputPort<float4>("Value").Build();
        }

        protected override void EmitHLSL(ComputeGraphCompiler compiler, string outputVar)
        {
            string texture = EvaluateInput(compiler, "RWTexture");
            string indices = EvaluateInput(compiler, "Indices");
            string value = EvaluateInput(compiler, "Value");

            compiler.Body.AppendLine($"    {texture}[{indices}] = {value};");
        }
    }
}