using System;
using Unity.GraphToolkit.Editor;
using Unity.Mathematics;

namespace Editor.Nodes.Inputs
{
    [Serializable]
    [Node("Inputs", "", "Group Thread ID", StylePath)]
    public class GroupThreadId : ComputeNodeBase
    {
        protected override void OnDefinePorts(IPortDefinitionContext context)
        {
            context.AddOutputPort<int3>("XYZ").Build();
            
            context.AddOutputPort<int>("X").Build();
            context.AddOutputPort<int>("Y").Build();
            context.AddOutputPort<int>("Z").Build();
        }

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