using System;
using Unity.GraphToolkit.Editor;

namespace Editor.Nodes.Control
{
    [Serializable]
    [Node("Control", "", "If")]
    public class IfNode : ComputeNodeWildcardBase
    {
        public override string[] wildcardPorts => new[] { "True", "False" };
    
        protected override void OnDefinePorts(IPortDefinitionContext context)
        {
            context.AddInputPort("True").WithDataType(resolvedType).Build();
            context.AddInputPort("False").WithDataType(resolvedType).Build();

            context.AddInputPort<bool>("In").Build();

            context.AddOutputPort("Out").WithDataType(resolvedType).Build();
        }

        protected override void EmitHLSL(ComputeGraphCompiler compiler, string outputVar)
        {
            string valTrue = EvaluateInput(compiler, "True");
            string valFalse = EvaluateInput(compiler, "False");
            string valIn = EvaluateInput(compiler, "In");

            string hlslType = ComputeGraphTypes.GetStringFromCSType(resolvedType);
            
            compiler.Body.AppendLine($"    {hlslType} {outputVar} = {valIn} ? {valTrue} : {valFalse};");
        }
    }
}