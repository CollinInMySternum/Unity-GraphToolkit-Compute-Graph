using System;
using System.Collections.Generic;
using Unity.GraphToolkit.Editor;
using UnityEditor.Experimental.GraphView;

namespace Editor.Nodes.Math.Advanced
{
    [Serializable]
    [Node("Math/Advanced", "", "Power", StylePath)]
    public class PowerNode : ComputeNodeBase
    {
        protected override IEnumerable<PortDefinition> DefinedPorts => new[] 
        {
            PortDefinition.Input("In", type: typeof(float)),
            PortDefinition.Input("Exp", type: typeof(float)),
            PortDefinition.Output("Out", type: typeof(float))
        };

        protected override void EmitHLSL(ComputeGraphCompiler compiler, string outputVar)
        {
            string valIn = EvaluateInput(compiler, "In");
            string valExp = EvaluateInput(compiler, "Exp");

            compiler.Body.AppendLine($"    float {outputVar} = pow({valIn}, {valExp});");
        }
    }
}