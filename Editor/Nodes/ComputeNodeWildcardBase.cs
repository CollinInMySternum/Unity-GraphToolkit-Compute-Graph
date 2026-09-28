using System;
using System.Collections.Generic;
using System.Linq;
using Unity.GraphToolkit.Editor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Editor.Nodes
{
    [Serializable]
    [Node("", "", "", StylePath)]
    public abstract class ComputeNodeWildcardBase : ComputeNodeBase
    {
        [NonSerialized] private Type _cachedResolvedType;
        
        public virtual bool IsValidWildcardType(Type type)
        {
            return !ComputeGraphTypes.IsBuffer(type) && !ComputeGraphTypes.IsTexture(type);
        }
        
        public virtual string[] wildcardPorts => DefinedPorts
            .Where(p => p.IsWildcard)
            .Select(p => p.Name)
            .ToArray();

        public void CacheWildcardState()
        {
            _cachedResolvedType = null;
            foreach (var portName in wildcardPorts)
            {
                var port = GetInputPortByName(portName);
                if (port != null)
                {
                    var connectedPorts = new List<IPort>();
                    port.GetConnectedPorts(connectedPorts);
                        
                    // If something is connected, adopt its type
                    if (connectedPorts.Count > 0)
                    {
                        _cachedResolvedType = connectedPorts[0].DataType;
                        return;
                    }
                }
            }
            return; // Nothing connected - return null
        }

        public Type ResolvedType => _cachedResolvedType;

        protected override Type ResolvePortType(PortDefinition def)
        {
            Type currentResolved = ResolvedType;
            
            if (def.IsWildcard)
            {
                return currentResolved ?? def.StaticType;
            }

            if (def.DynamicTypeResolver != null)
            {
                if (currentResolved == null || currentResolved == typeof(Untyped))
                {
                    return def.StaticType;
                }
                return def.DynamicTypeResolver(currentResolved) ?? def.StaticType;
            }

            return def.StaticType;
        }
    }
}