using System;
using Unity.GraphToolkit.Editor;

namespace Editor.Nodes
{
    [Serializable]
    public class TypeRegistrationDummyNode : ComputeNodeBase
    {
        protected override void OnDefinePorts(IPortDefinitionContext context)
        {
            foreach (Type resourceType in ComputeGraphTypes.Resources.Keys)
            {
                context.AddInputPort(resourceType.Name).WithDataType(resourceType).Build();
            }
        }

        protected override void EmitHLSL(ComputeGraphCompiler compiler, string outputVar)
        {
            // Nothing here
        }
    }
}