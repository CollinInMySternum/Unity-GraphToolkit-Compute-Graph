using System;
using Unity.GraphToolkit.Editor;

namespace Editor.Nodes.Math.Trigonometry
{
    [Serializable]
    [Node("Math/Trigonometry", "", "Tangent (Degrees)", StylePath)]
    public class TangentDegrees : ComputeNodeBase
    {
        protected override void OnDefinePorts(IPortDefinitionContext context)
        {
            context.AddInputPort<float>("In").Build();
            context.AddOutputPort<float>("Out").Build();
        }

        protected override void EmitHLSL(ComputeGraphCompiler compiler, string outputVar)
        {
            string valX = EvaluateInput(compiler, "In");

            compiler.Body.AppendLine($"    float {outputVar} = tan({valX} * PI / 180.0f);");
        }
    }

    [Serializable]
    [Node("Math/Trigonometry", "", "Tangent (Radians)", StylePath)]
    public class TangentRadians : ComputeNodeBase
    {
        protected override void OnDefinePorts(IPortDefinitionContext context)
        {
            context.AddInputPort<float>("In").Build();
            context.AddOutputPort<float>("Out").Build();
        }

        protected override void EmitHLSL(ComputeGraphCompiler compiler, string outputVar)
        {
            string valX = EvaluateInput(compiler, "In");

            compiler.Body.AppendLine($"    float {outputVar} = tan({valX} * 180.0f / PI);");
        }
    }
    
    [Serializable]
    [Node("Math/Trigonometry", "", "Arctangent (Degrees)", StylePath)]
    public class ArcTangentDegrees : ComputeNodeBase
    {
        protected override void OnDefinePorts(IPortDefinitionContext context)
        {
            context.AddInputPort<float>("In").Build();
            context.AddOutputPort<float>("Out").Build();
        }

        protected override void EmitHLSL(ComputeGraphCompiler compiler, string outputVar)
        {
            string valX = EvaluateInput(compiler, "In");

            compiler.Body.AppendLine($"    float {outputVar} = atan({valX} * PI / 180.0f);");
        }
    }

    [Serializable]
    [Node("Math/Trigonometry", "", "Arctangent (Radians)", StylePath)]
    public class ArcTangentRadians : ComputeNodeBase
    {
        protected override void OnDefinePorts(IPortDefinitionContext context)
        {
            context.AddInputPort<float>("In").Build();
            context.AddOutputPort<float>("Out").Build();
        }

        protected override void EmitHLSL(ComputeGraphCompiler compiler, string outputVar)
        {
            string valX = EvaluateInput(compiler, "In");

            compiler.Body.AppendLine($"    float {outputVar} = atan({valX} * 180.0f / PI);");
        }
    }
}