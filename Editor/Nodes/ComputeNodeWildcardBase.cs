using System;
using System.Collections.Generic;
using Unity.GraphToolkit.Editor;
using UnityEngine;

namespace Editor.Nodes
{
    [Serializable]
    [Node("", "", "", StylePath)]
    public abstract class ComputeNodeWildcardBase : ComputeNodeBase
    {
        [SerializeField] public Type resolvedType = typeof(Untyped);
        public abstract string[] wildcardPorts { get; }
        
        public virtual bool IsValidWildcardType(Type type)
        {
            return !ComputeGraphTypes.IsBuffer(type) && !ComputeGraphTypes.IsTexture(type);
        }

        public void ResolveType()
        {
            Type newType = typeof(Untyped);

            foreach (var portName in wildcardPorts)
            {
                var port = GetInputPortByName(portName);
                if (port == null) continue;
                
                var connectedPorts = new List<IPort>();
                port.GetConnectedPorts(connectedPorts);

                if (connectedPorts.Count > 0)
                {
                    Type candidateType = connectedPorts[0].DataType;

                    if (IsValidWildcardType(candidateType))
                    {
                        newType = candidateType;
                    }
                }
            }

            // Ports did not change
            if (newType == resolvedType) return;
            
            // Refresh ports, port type changed
            resolvedType = newType;
            DefineNode();
        }
    }
}