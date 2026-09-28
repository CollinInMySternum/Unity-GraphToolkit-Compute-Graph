using System;
using System.Collections.Generic;
using Unity.GraphToolkit.Editor;
using Unity.Mathematics;
using UnityEngine;

namespace Editor.Nodes.Textures
{
    [Serializable]
    [Node("Textures", "", "Texture Read", StylePath)]
    public class TextureReadNode : ComputeNodeWildcardBase
    {
        public override bool IsValidWildcardType(Type type)
        {
            return ComputeGraphTypes.IsTexture(type);
        }
        
        protected override IEnumerable<PortDefinition> DefinedPorts => new[]
        {
            PortDefinition.Input("Texture", isWildcard: true),
            PortDefinition.Input("Indices", dynamicTypeResolver: t => 
                ResolvedType == typeof(Texture2D) ? typeof(int2) :
                ResolvedType == typeof(Texture3D) ? typeof(int3) :
                ResolvedType == typeof(ComputeGraphTypes.RWTexture2D) ? typeof(int2) :
                ResolvedType == typeof(ComputeGraphTypes.RWTexture3D) ? typeof(int3) :
                typeof(Untyped)
            ),
            
            PortDefinition.Output("Out", typeof(float4))
        };

        protected override void EmitHLSL(ComputeGraphCompiler compiler, string outputVar)
        {
            string texture = EvaluateInput(compiler, "Texture");
            string indices = EvaluateInput(compiler, "Indices");

            compiler.Body.AppendLine($"    float4 {outputVar} = {texture}[{indices}].rgba;");
        }
    }
}