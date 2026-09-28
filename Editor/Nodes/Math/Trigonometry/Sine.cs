using System;
using System.Collections.Generic;
using Unity.GraphToolkit.Editor;

namespace Editor.Nodes.Math.Trigonometry
{
    [Serializable]
    [Node("Math/Trigonometry", "", "Sine (Degrees)", StylePath)]
    public class SineDegrees : ComputeNodeBase
    {
        protected override IEnumerable<PortDefinition> DefinedPorts => new[]
        {
            PortDefinition.Input("In", type: typeof(float)),
            PortDefinition.Output("Out", type: typeof(float)),
        };

        protected override void EmitHLSL(ComputeGraphCompiler compiler, string outputVar)
        {
            string valX = EvaluateInput(compiler, "In");

            compiler.Body.AppendLine($"    float {outputVar} = sin({valX} * PI / 180.0f);");
        }
    }

    [Serializable]
    [Node("Math/Trigonometry", "", "Sine (Radians)", StylePath)]
    public class SineRadians : ComputeNodeBase
    {
        protected override IEnumerable<PortDefinition> DefinedPorts => new[]
        {
            PortDefinition.Input("In", type: typeof(float)),
            PortDefinition.Output("Out", type: typeof(float)),
        };

        protected override void EmitHLSL(ComputeGraphCompiler compiler, string outputVar)
        {
            string valX = EvaluateInput(compiler, "In");

            compiler.Body.AppendLine($"    float {outputVar} = sin({valX});");
        }
    }
    
    [Serializable]
    [Node("Math/Trigonometry", "", "Arcsine (Degrees)", StylePath)]
    public class ArcSineDegrees : ComputeNodeBase
    {
        protected override IEnumerable<PortDefinition> DefinedPorts => new[]
        {
            PortDefinition.Input("In", type: typeof(float)),
            PortDefinition.Output("Out", type: typeof(float)),
        };

        protected override void EmitHLSL(ComputeGraphCompiler compiler, string outputVar)
        {
            string valX = EvaluateInput(compiler, "In");

            compiler.Body.AppendLine($"    float {outputVar} = asin({valX} * PI / 180.0f);");
        }
    }

    [Serializable]
    [Node("Math/Trigonometry", "", "Arcsine (Radians)", StylePath)]
    public class ArcSineRadians : ComputeNodeBase
    {
        protected override IEnumerable<PortDefinition> DefinedPorts => new[]
        {
            PortDefinition.Input("In", type: typeof(float)),
            PortDefinition.Output("Out", type: typeof(float)),
        };

        protected override void EmitHLSL(ComputeGraphCompiler compiler, string outputVar)
        {
            string valX = EvaluateInput(compiler, "In");

            compiler.Body.AppendLine($"    float {outputVar} = asin({valX});");
        }
    }
}