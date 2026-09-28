using System;
using System.Collections.Generic;
using Unity.GraphToolkit.Editor;
using UnityEngine;

namespace Editor.Nodes.Buffers
{
    [Serializable]
    [Node("Buffers", "", "Buffer Write", StylePath)]
    public class BufferWrite : ComputeNodeWildcardBase, IComputeNodeOutput
    {
        public override bool IsValidWildcardType(Type type)
        {
            return ComputeGraphTypes.IsBuffer(type) && ComputeGraphTypes.IsReadWrite(type);
        }
        
        protected override IEnumerable<PortDefinition> DefinedPorts => new[]
        {
            PortDefinition.Input("RWBuffer", ui: PortConnectorUI.Arrowhead, isWildcard: true),
            PortDefinition.Input("Index", type: typeof(int)),
            PortDefinition.Input("Value", dynamicTypeResolver: t=> ComputeGraphTypes.GetPayloadType(t))
        };

        protected override void EmitHLSL(ComputeGraphCompiler compiler, string outputVar)
        {
            string bufferName = EvaluateInput(compiler, "RWBuffer");
            string index = EvaluateInput(compiler, "Index", "0");
            
            string value = EvaluateInput(compiler, "Value");

            compiler.Body.AppendLine($"    {bufferName}[{index}] = {value};");
        }
    }
}