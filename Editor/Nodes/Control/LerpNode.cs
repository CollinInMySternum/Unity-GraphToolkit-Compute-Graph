using System;
using Unity.GraphToolkit.Editor;

namespace Editor.Nodes.Control
{
    [Serializable]
    [Node("Control", "", "Lerp")]
    public class LerpNode : ComputeNodeWildcardBase
    {
        public override string[] wildcardPorts => new[] { "A", "B" };

        public override bool IsValidWildcardType(Type type)
        {
            // Only support data types
            return !ComputeGraphTypes.IsBuffer(type) && !ComputeGraphTypes.IsTexture(type);
        }

        protected override void OnDefinePorts(IPortDefinitionContext context)
        {
            context.AddInputPort("A").WithDataType(resolvedType).Build();
            context.AddInputPort("B").WithDataType(resolvedType).Build();

            context.AddInputPort<float>("X").Build();

            context.AddOutputPort("Out").WithDataType(resolvedType).Build();
        }

        protected override void EmitHLSL(ComputeGraphCompiler compiler, string outputVar)
        {
            string valA = EvaluateInput(compiler, "A");
            string valB = EvaluateInput(compiler, "B");
            string valX = EvaluateInput(compiler, "X");

            string hlslType = ComputeGraphTypes.GetStringFromCSType(resolvedType);
            
            compiler.Body.AppendLine($"    {hlslType} {outputVar} = lerp({valA}, {valB}, {valX});");
        }
    }
}