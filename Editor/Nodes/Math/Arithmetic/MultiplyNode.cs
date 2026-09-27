using System;
using Unity.GraphToolkit.Editor;
using Unity.Mathematics;
using UnityEngine;

namespace Editor.Nodes.Math.Arithmetic
{
    [Serializable]
    [Node("Math/Arithmetic", "", "Multiply", StylePath)]
    public class MultiplyNode : ComputeNodeWildcardBase
    {
        public override string[] wildcardPorts => new[] { "A", "B", "Out" };

        protected override void OnDefinePorts(IPortDefinitionContext context)
        {
            context.AddInputPort("A").WithDataType(resolvedType).Build();
            context.AddInputPort("B").WithDataType(resolvedType).Build();
            
            context.AddOutputPort("Out").WithDataType(resolvedType).Build();
        }

        protected override void EmitHLSL(ComputeGraphCompiler compiler, string outputVar)
        {
            string valA = EvaluateInput(compiler, "A");
            string valB = EvaluateInput(compiler, "B");

            string hlslType = ComputeGraphTypes.GetStringFromCSType(resolvedType);

            compiler.Body.AppendLine($"    {hlslType} {outputVar} = {valA} * {valB};");
        }
    }
}