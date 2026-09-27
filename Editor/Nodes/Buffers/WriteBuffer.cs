using System;
using Unity.GraphToolkit.Editor;
using UnityEngine;

namespace Editor.Nodes.Buffers
{
    [Serializable]
    [Node("Buffers", "", "Write Buffer", StylePath)]
    public class WriteBuffer : ComputeNodeWildcardBase
    {
        public override string[] wildcardPorts => new[] { "RWBuffer" };

        public override bool IsValidWildcardType(Type type)
        {
            return ComputeGraphTypes.IsBuffer(type) && ComputeGraphTypes.IsReadWrite(type);
        }

        protected override void OnDefinePorts(IPortDefinitionContext context)
        {
            context.AddInputPort("RWBuffer")
                .WithDataType(resolvedType)
                .WithConnectorUI(PortConnectorUI.Arrowhead)
                .Build();
            
            context.AddInputPort<int>("Index").Build();
            
            context.AddInputPort("Value")
                .WithDataType(ComputeGraphTypes.GetPayloadType(resolvedType))
                .Build();
        }

        protected override void EmitHLSL(ComputeGraphCompiler compiler, string outputVar)
        {
            string bufferName = EvaluateInput(compiler, "RWBuffer");
            string index = EvaluateInput(compiler, "Index", "0");
            
            string value = EvaluateInput(compiler, "Value");

            compiler.Body.AppendLine($"    {bufferName}[{index}] = {value};");
        }
    }
}