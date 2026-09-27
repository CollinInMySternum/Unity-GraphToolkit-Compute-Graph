using System;
using Unity.GraphToolkit.Editor;
using UnityEngine;

namespace Editor.Nodes.Buffers
{
    [Serializable]
    [Node("Buffers", "", "Read Buffer", StylePath)]
    public class ReadBuffer : ComputeNodeWildcardBase
    {
        public override string[] wildcardPorts => new[] { "Buffer" };

        public override bool IsValidWildcardType(Type type)
        {
            return ComputeGraphTypes.IsBuffer(type);
        }

        protected override void OnDefinePorts(IPortDefinitionContext context)
        {
            context.AddInputPort("Buffer")
                .WithDataType(resolvedType)
                .WithConnectorUI(PortConnectorUI.Arrowhead)
                .Build();
            
            context.AddInputPort<int>("Index")
                .Build();

            context.AddOutputPort("Out")
                .WithDataType(ComputeGraphTypes.GetPayloadType(resolvedType))
                .Build();
        }
        
        protected override void EmitHLSL(ComputeGraphCompiler compiler, string outputVar)
        {
            string bufferName = EvaluateInput(compiler, "Buffer");
            string index = EvaluateInput(compiler, "Index", "0");

            Type payloadType = ComputeGraphTypes.GetPayloadType(resolvedType);
            var hlslType = ComputeGraphTypes.GetStringFromCSType(payloadType);

            compiler.Body.AppendLine($"    {hlslType} {outputVar} = {bufferName}[{index}];");
        }
    }
}