using System;
using System.Collections.Generic;
using Unity.GraphToolkit.Editor;
using Unity.Mathematics;

namespace Editor.Nodes.Inputs
{
    [Serializable]
    [Node("Inputs", "", "Dispatch Thread ID", StylePath)]
    public class DispatchThreadId : ComputeNodeBase
    {
        protected override IEnumerable<PortDefinition> DefinedPorts => new[]
        {
            PortDefinition.Output("XYZ", type: typeof(int3)),
            PortDefinition.Output("X", type: typeof(int)),
            PortDefinition.Output("Y", type: typeof(int)),
            PortDefinition.Output("Z", type: typeof(int)),
        };
        
        protected override void EmitHLSL(ComputeGraphCompiler compiler, string outputVar)
        {
            // Doesn't emit code, since it just accesses a variable
        }
        
        protected override string FormatOutputVariable(string baseVarName, string outPortName)
        {
            switch (outPortName)
            {
                case "XYZ": return "dispatchThreadID";
                case "X": return "dispatchThreadID.x";
                case "Y": return "dispatchThreadID.y";
                case "Z": return "dispatchThreadID.z";
                
                default: return "0";
            }
        }
    }
}