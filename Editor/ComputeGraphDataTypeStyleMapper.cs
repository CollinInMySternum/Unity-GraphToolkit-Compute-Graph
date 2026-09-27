using Unity.GraphToolkit.Editor;
using Unity.Mathematics;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Editor
{
    [DataTypeStyleMapper(typeof(ComputeGraph))]
    public class ComputeGraphDataTypeStyleMapper : DataTypeStyleMapper
    {
        public struct DataTypeStyleDefinition
        {
            public Type Type;
            public string IconPath;
            public Color Color;
        }

        public static Color SingleColor = new Color(0.501f, 1.000f, 0.901f); // #80FFE6
        public static Color Vector2Color = new Color(0.078f, 0.827f, 0.407f); // #80FFE6
        public static Color Vector3Color = new Color(1.000f, 0.907f, 0.027f); // #80FFE6
        public static Color Vector4Color = new Color(0.905f, 0.552f, 0.862f); // #80FFE6
        
        protected List<DataTypeStyleDefinition> TypeDefinitions = new List<DataTypeStyleDefinition>()
        {
            new DataTypeStyleDefinition { Type = typeof(int2), IconPath = "Vector2@4x", Color = SingleColor },
            new DataTypeStyleDefinition { Type = typeof(int3), IconPath = "Vector3@4x", Color = Vector2Color },
            new DataTypeStyleDefinition { Type = typeof(int4), IconPath = "Vector4@4x", Color = Vector3Color },
            
            new DataTypeStyleDefinition { Type = typeof(ComputeGraphTypes.RWBufferFloat), IconPath = "Advanced@4x", Color = SingleColor },
            new DataTypeStyleDefinition { Type = typeof(ComputeGraphTypes.RWBufferFloat2), IconPath = "Advanced@4x", Color = Vector2Color },
            new DataTypeStyleDefinition { Type = typeof(ComputeGraphTypes.RWBufferFloat3), IconPath = "Advanced@4x", Color = Vector3Color },
            new DataTypeStyleDefinition { Type = typeof(ComputeGraphTypes.RWBufferFloat4), IconPath = "Advanced@4x", Color = Vector4Color },
                                                                                                                                         
            new DataTypeStyleDefinition { Type = typeof(ComputeGraphTypes.BufferFloat), IconPath = "Advanced@4x", Color = SingleColor },
            new DataTypeStyleDefinition { Type = typeof(ComputeGraphTypes.BufferFloat2), IconPath = "Advanced@4x", Color = Vector2Color },
            new DataTypeStyleDefinition { Type = typeof(ComputeGraphTypes.BufferFloat3), IconPath = "Advanced@4x", Color = Vector3Color },
            new DataTypeStyleDefinition { Type = typeof(ComputeGraphTypes.BufferFloat4), IconPath = "Advanced@4x", Color = Vector4Color },
        };
        
        public ComputeGraphDataTypeStyleMapper()
        {
            foreach (var definition in TypeDefinitions)
            {
                var iconContent = EditorGUIUtility.IconContent(definition.IconPath);
                var iconTexture = iconContent?.image as Texture2D;
                
                Register(definition.Type, iconTexture, definition.Color);
            }
        }
    }
}