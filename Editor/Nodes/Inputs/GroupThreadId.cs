using System;
using System.Collections.Generic;
using Unity.GraphToolkit.Editor;
using Unity.Mathematics;

namespace Editor.Nodes.Inputs
{
    [Serializable]
    [Node("Inputs", "", "Group Thread ID", StylePath)]
    public class GroupThreadId : ComputeNodeBase
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
                case "XYZ": return "groupThreadID";
                case "X": return "groupThreadID.x";
                case "Y": return "groupThreadID.y";
                case "Z": return "groupThreadID.z";
                
                default: return "0";
            }
        }
    }
}