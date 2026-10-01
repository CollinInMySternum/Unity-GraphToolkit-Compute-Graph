using System;
using System.Collections.Generic;
using Unity.GraphToolkit.Editor;
using UnityEditor.Experimental.GraphView;

namespace Editor.Nodes.Math.Advanced
{
    [Serializable]
    [Node("Math/Advanced", "", "Square Root", StylePath)]
    public class SquareRootNode : ComputeNodeBase
    {
        protected override IEnumerable<PortDefinition> DefinedPorts => new[] 
        {
            PortDefinition.Input("In", type: typeof(float)),
            PortDefinition.Output("Out", type: typeof(float))
        };

        protected override void EmitHLSL(ComputeGraphCompiler compiler, string outputVar)
        {
            string valIn = EvaluateInput(compiler, "In");

            compiler.Body.AppendLine($"    float {outputVar} = sqrt({valIn});");
        }
    }
}