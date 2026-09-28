using System;
using System.Collections.Generic;
using Unity.GraphToolkit.Editor;

namespace Editor.Nodes.Control
{
    [Serializable]
    [Node("Control", "", "If")]
    public class IfNode : ComputeNodeWildcardBase
    {
        protected override IEnumerable<PortDefinition> DefinedPorts => new[]
        {
            PortDefinition.Input("True", isWildcard: true),
            PortDefinition.Input("False", isWildcard: true),

            PortDefinition.Input("In", type: typeof(bool)),

            PortDefinition.Output("Out", isWildcard: true)
        };

        protected override void EmitHLSL(ComputeGraphCompiler compiler, string outputVar)
        {
            string valTrue = EvaluateInput(compiler, "True");
            string valFalse = EvaluateInput(compiler, "False");
            string valIn = EvaluateInput(compiler, "In");

            string hlslType = ComputeGraphTypes.GetStringFromCSType(ResolvedType);
            
            compiler.Body.AppendLine($"    {hlslType} {outputVar} = {valIn} ? {valTrue} : {valFalse};");
        }
    }
}