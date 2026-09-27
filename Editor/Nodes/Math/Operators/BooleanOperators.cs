using System;
using Unity.GraphToolkit.Editor;

namespace Editor.Nodes.Math.Operators
{
    [Serializable]
    [Node("Math/Operators", "", "OR", StylePath)]
    public class BooleanOr : ComputeNodeBase
    {
        protected override void OnDefinePorts(IPortDefinitionContext context)
        {
            context.AddInputPort<bool>("A").Build();
            context.AddInputPort<bool>("B").Build();

            context.AddOutputPort<bool>("Out").Build();
        }

        protected override void EmitHLSL(ComputeGraphCompiler compiler, string outputVar)
        {
            string valA = EvaluateInput(compiler, "A");
            string valB = EvaluateInput(compiler, "B");

            compiler.Body.AppendLine($"    bool {outputVar} = {valA} || {valB};");
        }
    }
    
    [Serializable]
    [Node("Math/Operators", "", "AND", StylePath)]
    public class BooleanAnd : ComputeNodeBase
    {
        protected override void OnDefinePorts(IPortDefinitionContext context)
        {
            context.AddInputPort<bool>("A").Build();
            context.AddInputPort<bool>("B").Build();
            
            context.AddOutputPort<bool>("Out").Build();
        }

        protected override void EmitHLSL(ComputeGraphCompiler compiler, string outputVar)
        {
            string valA = EvaluateInput(compiler, "A");
            string valB = EvaluateInput(compiler, "B");

            compiler.Body.AppendLine($"    bool {outputVar} = {valA} && {valB};");
        }
    }
}