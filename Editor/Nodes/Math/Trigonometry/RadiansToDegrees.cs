using System;
using System.Collections.Generic;
using Unity.GraphToolkit.Editor;

namespace Editor.Nodes.Math.Trigonometry
{
    [Serializable]
    [Node("Math/Trigonometry", "", "Radians to Degrees", StylePath)]
    public class RadiansToDegrees : ComputeNodeBase
    {
        protected override IEnumerable<PortDefinition> DefinedPorts => new[]
        {
            PortDefinition.Input("In", type: typeof(float)),
            PortDefinition.Output("Out", type: typeof(float)),
        };

        protected override void EmitHLSL(ComputeGraphCompiler compiler, string outputVar)
        {
            string valX = EvaluateInput(compiler, "In");

            compiler.Body.AppendLine($"    float {outputVar} = {valX} * PI / 180.0f;");
        }
    }
    
    [Serializable]
    [Node("Math/Trigonometry", "", "Degrees to Radians", StylePath)]
    public class DegreesToRadians : ComputeNodeBase
    {
        protected override IEnumerable<PortDefinition> DefinedPorts => new[]
        {
            PortDefinition.Input("In", type: typeof(float)),
            PortDefinition.Output("Out", type: typeof(float)),
        };

        protected override void EmitHLSL(ComputeGraphCompiler compiler, string outputVar)
        {
            string valX = EvaluateInput(compiler, "In");

            compiler.Body.AppendLine($"    float {outputVar} = {valX} * 180.0f / PI;");
        }
    }
}