using System;
using Unity.GraphToolkit.Editor;

namespace Editor.Nodes.Math.Trigonometry
{
    [Serializable]
    [Node("Math/Trigonometry", "", "Radians to Degrees", StylePath)]
    public class RadiansToDegrees : ComputeNodeBase
    {
        protected override void OnDefinePorts(IPortDefinitionContext context)
        {
            context.AddInputPort<float>("In (Radians)").Build();

            context.AddOutputPort<float>("Out (Degrees)").Build();
        }

        protected override void EmitHLSL(ComputeGraphCompiler compiler, string outputVar)
        {
            string valX = EvaluateInput(compiler, "In (Radians)");

            compiler.Body.AppendLine($"    float {outputVar} = {valX} * PI / 180.0f;");
        }
    }
    
    [Serializable]
    [Node("Math/Trigonometry", "", "Degrees to Radians", StylePath)]
    public class DegreesToRadians : ComputeNodeBase
    {
        protected override void OnDefinePorts(IPortDefinitionContext context)
        {
            context.AddInputPort<float>("In (Degrees)").Build();

            context.AddOutputPort<float>("Out (Radians)").Build();
        }

        protected override void EmitHLSL(ComputeGraphCompiler compiler, string outputVar)
        {
            string valX = EvaluateInput(compiler, "In (Degrees)");

            compiler.Body.AppendLine($"    float {outputVar} = {valX} * 180.0f / PI;");
        }
    }
}