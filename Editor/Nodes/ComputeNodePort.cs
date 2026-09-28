using System;
using System.Collections.Generic;
using System.Linq;
using Unity.GraphToolkit.Editor;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace Editor.Nodes
{
    public struct PortDefinition
    {
        public string Name { get; set; }
        public PortDirection Direction { get; set; }
        public Type StaticType { get; set; }
        public PortConnectorUI ConnectorUI { get; set; }
        public bool IsWildcard { get; set;}
        
        public Func<Type, Type> DynamicTypeResolver { get; set; }

        public PortDefinition(
            string name,
            PortDirection direction,
            Type staticType = null,
            PortConnectorUI connectorUI = PortConnectorUI.Circle,
            bool isWildcard = false,
            Func<Type, Type> dynamicTypeResolver = null)
        {
            Name = name;
            Direction = direction;
            StaticType = staticType ?? typeof(Untyped);
            ConnectorUI = connectorUI;
            IsWildcard = isWildcard;
            DynamicTypeResolver = dynamicTypeResolver;
        }
        
        public static PortDefinition Input(
            string name, 
            Type type = null, 
            PortConnectorUI ui = PortConnectorUI.Circle, 
            bool isWildcard = false,
            Func<Type, Type> dynamicTypeResolver = null) =>
                new PortDefinition(name, PortDirection.Input, type, ui, isWildcard, dynamicTypeResolver);
        
        public static PortDefinition Output(
            string name, 
            Type type = null, 
            PortConnectorUI ui = PortConnectorUI.Circle, 
            bool isWildcard = false,
            Func<Type, Type> dynamicTypeResolver = null) =>
                new PortDefinition(name, PortDirection.Output, type, ui, isWildcard, dynamicTypeResolver);
    }
}