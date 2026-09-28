using System;
using System.Collections.Generic;
using Unity.GraphToolkit.Editor;
using UnityEngine;

namespace Editor.Nodes.Buffers
{
    [Serializable]
    [Node("Buffers", "", "Buffer Read", StylePath)]
    public class BufferRead : ComputeNodeWildcardBase
    {
        public override bool IsValidWildcardType(Type type) => ComputeGraphTypes.IsBuffer(type);

        protected override IEnumerable<PortDefinition> DefinedPorts => new[]
        {
            PortDefinition.Input("Buffer", ui: PortConnectorUI.Arrowhead, isWildcard: true),
            PortDefinition.Input("Index", type: typeof(int)),
            
            PortDefinition.Output("Output", dynamicTypeResolver: t=> ComputeGraphTypes.GetPayloadType(t))
        };

        protected override void EmitHLSL(ComputeGraphCompiler compiler, string outputVar)
        {
            string bufferName = EvaluateInput(compiler, "Buffer");
            string index = EvaluateInput(compiler, "Index", "0");

            Type payloadType = ComputeGraphTypes.GetPayloadType(ResolvedType);
            var hlslType = ComputeGraphTypes.GetStringFromCSType(payloadType);

            compiler.Body.AppendLine($"    {hlslType} {outputVar} = {bufferName}[{index}];");
        }
    }
}