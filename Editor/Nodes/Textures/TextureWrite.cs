using System;
using System.Collections.Generic;
using Unity.GraphToolkit.Editor;
using Unity.Mathematics;
using UnityEngine;

namespace Editor.Nodes.Textures
{
    [Serializable]
    [Node("Textures", "", "Texture Write", StylePath)]
    public class TextureWriteNode : ComputeNodeWildcardBase, IComputeNodeOutput
    {
        public override bool IsValidWildcardType(Type type)
        {
            return ComputeGraphTypes.IsTexture(type) && ComputeGraphTypes.IsReadWrite(type);
        }
        
        protected override IEnumerable<PortDefinition> DefinedPorts => new[]
        {
            PortDefinition.Input("RWTexture", isWildcard: true),
            PortDefinition.Input("Indices", dynamicTypeResolver: t => 
                ResolvedType == typeof(ComputeGraphTypes.RWTexture2D) ? typeof(int2) :
                ResolvedType == typeof(ComputeGraphTypes.RWTexture3D) ? typeof(int3) :
                typeof(Untyped)
            ),
            
            PortDefinition.Input("Value", type: typeof(float4))
        };

        protected override void EmitHLSL(ComputeGraphCompiler compiler, string outputVar)
        {
            string texture = EvaluateInput(compiler, "RWTexture");
            string indices = EvaluateInput(compiler, "Indices");
            string value = EvaluateInput(compiler, "Value");

            compiler.Body.AppendLine($"    {texture}[{indices}] = {value};");
        }
    }
}