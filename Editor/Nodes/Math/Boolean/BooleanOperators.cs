using System;
using System.Collections.Generic;
using Unity.GraphToolkit.Editor;

namespace Editor.Nodes.Math.Boolean
{
    [Serializable]
    [Node("Math/Boolean", "", "OR", StylePath)]
    public class BooleanOr : ComputeNodeBase
    {
        protected override IEnumerable<PortDefinition> DefinedPorts => new[]
        {
            PortDefinition.Input("A", type: typeof(bool)),
            PortDefinition.Input("B", type: typeof(bool)),
            
            PortDefinition.Output("Out", type: typeof(bool))
        };

        protected override void EmitHLSL(ComputeGraphCompiler compiler, string outputVar)
        {
            string valA = EvaluateInput(compiler, "A");
            string valB = EvaluateInput(compiler, "B");

            compiler.Body.AppendLine($"    bool {outputVar} = {valA} || {valB};");
        }
    }
    
    [Serializable]
    [Node("Math/Boolean", "", "AND", StylePath)]
    public class BooleanAnd : ComputeNodeBase
    {
        
        protected override IEnumerable<PortDefinition> DefinedPorts => new[]
        {
            PortDefinition.Input("A", type: typeof(bool)),
            PortDefinition.Input("B", type: typeof(bool)),
            
            PortDefinition.Output("Out", type: typeof(bool))
        };

        protected override void EmitHLSL(ComputeGraphCompiler compiler, string outputVar)
        {
            string valA = EvaluateInput(compiler, "A");
            string valB = EvaluateInput(compiler, "B");

            compiler.Body.AppendLine($"    bool {outputVar} = {valA} && {valB};");
        }
    }
}