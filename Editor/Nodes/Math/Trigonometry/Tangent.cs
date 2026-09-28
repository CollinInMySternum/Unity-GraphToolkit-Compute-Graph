using System;
using System.Collections.Generic;
using Unity.GraphToolkit.Editor;

namespace Editor.Nodes.Math.Trigonometry
{
    [Serializable]
    [Node("Math/Trigonometry", "", "Tangent (Degrees)", StylePath)]
    public class TangentDegrees : ComputeNodeBase
    {
        protected override IEnumerable<PortDefinition> DefinedPorts => new[]
        {
            PortDefinition.Input("In", type: typeof(float)),
            PortDefinition.Output("Out", type: typeof(float)),
        };
        
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
        protected override IEnumerable<PortDefinition> DefinedPorts => new[]
        {
            PortDefinition.Input("In", type: typeof(float)),
            PortDefinition.Output("Out", type: typeof(float)),
        };

        protected override void EmitHLSL(ComputeGraphCompiler compiler, string outputVar)
        {
            string valX = EvaluateInput(compiler, "In");

            compiler.Body.AppendLine($"    float {outputVar} = tan({valX});");
        }
    }
    
    [Serializable]
    [Node("Math/Trigonometry", "", "Arctangent (Degrees)", StylePath)]
    public class ArcTangentDegrees : ComputeNodeBase
    {
        protected override IEnumerable<PortDefinition> DefinedPorts => new[]
        {
            PortDefinition.Input("In", type: typeof(float)),
            PortDefinition.Output("Out", type: typeof(float)),
        };

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
        protected override IEnumerable<PortDefinition> DefinedPorts => new[]
        {
            PortDefinition.Input("In", type: typeof(float)),
            PortDefinition.Output("Out", type: typeof(float)),
        };

        protected override void EmitHLSL(ComputeGraphCompiler compiler, string outputVar)
        {
            string valX = EvaluateInput(compiler, "In");

            compiler.Body.AppendLine($"    float {outputVar} = atan({valX});");
        }
    }
}