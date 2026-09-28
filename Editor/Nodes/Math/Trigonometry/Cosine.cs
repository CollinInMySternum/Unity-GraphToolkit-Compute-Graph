using System;
using System.Collections.Generic;
using Unity.GraphToolkit.Editor;

namespace Editor.Nodes.Math.Trigonometry
{
    [Serializable]
    [Node("Math/Trigonometry", "", "Cosine (Degrees)", StylePath)]
    public class CosineDegrees : ComputeNodeBase
    {
        protected override IEnumerable<PortDefinition> DefinedPorts => new[]
        {
            PortDefinition.Input("In", type: typeof(float)),
            PortDefinition.Output("Out", type: typeof(float)),
        };

        protected override void EmitHLSL(ComputeGraphCompiler compiler, string outputVar)
        {
            string valX = EvaluateInput(compiler, "In");

            compiler.Body.AppendLine($"    float {outputVar} = cos({valX} * PI / 180.0f);");
        }
    }

    [Serializable]
    [Node("Math/Trigonometry", "", "Cosine (Radians)", StylePath)]
    public class CosineRadians : ComputeNodeBase
    {
        protected override IEnumerable<PortDefinition> DefinedPorts => new[]
        {
            PortDefinition.Input("In", type: typeof(float)),
            PortDefinition.Output("Out", type: typeof(float)),
        };

        protected override void EmitHLSL(ComputeGraphCompiler compiler, string outputVar)
        {
            string valX = EvaluateInput(compiler, "In");

            compiler.Body.AppendLine($"    float {outputVar} = cos({valX});");
        }
    }
    
    [Serializable]
    [Node("Math/Trigonometry", "", "Arccosine (Degrees)", StylePath)]
    public class ArcCosineDegrees : ComputeNodeBase
    {
        protected override IEnumerable<PortDefinition> DefinedPorts => new[]
        {
            PortDefinition.Input("In", type: typeof(float)),
            PortDefinition.Output("Out", type: typeof(float)),
        };

        protected override void EmitHLSL(ComputeGraphCompiler compiler, string outputVar)
        {
            string valX = EvaluateInput(compiler, "In");

            compiler.Body.AppendLine($"    float {outputVar} = acos({valX} * PI / 180.0f);");
        }
    }

    [Serializable]
    [Node("Math/Trigonometry", "", "Arccosine (Radians)", StylePath)]
    public class ArcCosineRadians : ComputeNodeBase
    {
        protected override IEnumerable<PortDefinition> DefinedPorts => new[]
        {
            PortDefinition.Input("In", type: typeof(float)),
            PortDefinition.Output("Out", type: typeof(float)),
        };

        protected override void EmitHLSL(ComputeGraphCompiler compiler, string outputVar)
        {
            string valX = EvaluateInput(compiler, "In");

            compiler.Body.AppendLine($"    float {outputVar} = acos({valX});");
        }
    }
}