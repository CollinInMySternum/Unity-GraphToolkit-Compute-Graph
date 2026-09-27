using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using Unity.Mathematics;
using UnityEngine;

namespace Editor
{
    public static class ComputeGraphTypes
    {
        // Dummy types for Blackboard's UI
        [Serializable] public struct BufferFloat {}
        [Serializable] public struct BufferFloat2 {}
        [Serializable] public struct BufferFloat3 {}
        [Serializable] public struct BufferFloat4 {}
        [Serializable] public struct BufferInt {}

        [Serializable] public struct RWBufferFloat {}
        [Serializable] public struct RWBufferFloat2 {}
        [Serializable] public struct RWBufferFloat3 {}
        [Serializable] public struct RWBufferFloat4 {}
        [Serializable] public struct RWBufferInt {}
    
        public class ResourceMeta
        {
            public bool IsBuffer;
            public bool IsTexture;
            public bool IsReadWrite;
            public Type PayloadType;
            public string DeclarationTemplate;
        }
        
        public static readonly Dictionary<Type, ResourceMeta> Resources = new Dictionary<Type, ResourceMeta>
        {
            // Textures
            { typeof(Texture2D), new ResourceMeta { IsTexture = true, IsReadWrite = false, PayloadType = typeof(float4), DeclarationTemplate = "Texture2D<{0}> {1};\nSamplerState sampler_{1};" } },
            { typeof(RenderTexture), new ResourceMeta { IsTexture = true, IsReadWrite = true, PayloadType = typeof(float4), DeclarationTemplate = "RWTexture2D<{0}> {1};" } },
            { typeof(Texture3D), new ResourceMeta { IsTexture = true, IsReadWrite = false, PayloadType = typeof(float4), DeclarationTemplate = "Texture3D<{0}> {1};\nSamplerState sampler_{1};" } },
            
            // Readonly Buffers
            { typeof(BufferFloat), new ResourceMeta { IsBuffer = true, IsReadWrite = false, PayloadType = typeof(float), DeclarationTemplate = "StructuredBuffer<{0}> {1};" } },
            { typeof(BufferFloat2), new ResourceMeta { IsBuffer = true, IsReadWrite = false, PayloadType = typeof(float2), DeclarationTemplate = "StructuredBuffer<{0}> {1};" } },
            { typeof(BufferFloat3), new ResourceMeta { IsBuffer = true, IsReadWrite = false, PayloadType = typeof(float3), DeclarationTemplate = "StructuredBuffer<{0}> {1};" } },
            { typeof(BufferFloat4), new ResourceMeta { IsBuffer = true, IsReadWrite = false, PayloadType = typeof(float4), DeclarationTemplate = "StructuredBuffer<{0}> {1};" } },

            // Read/Write Buffers
            { typeof(RWBufferFloat), new ResourceMeta { IsBuffer = true, IsReadWrite = true, PayloadType = typeof(float), DeclarationTemplate = "RWStructuredBuffer<{0}> {1};" } },
            { typeof(RWBufferFloat2), new ResourceMeta { IsBuffer = true, IsReadWrite = true, PayloadType = typeof(float2), DeclarationTemplate = "RWStructuredBuffer<{0}> {1};" } },
            { typeof(RWBufferFloat3), new ResourceMeta { IsBuffer = true, IsReadWrite = true, PayloadType = typeof(float3), DeclarationTemplate = "RWStructuredBuffer<{0}> {1};" } },
            { typeof(RWBufferFloat4), new ResourceMeta { IsBuffer = true, IsReadWrite = true, PayloadType = typeof(float4), DeclarationTemplate = "RWStructuredBuffer<{0}> {1};" } },
        };
        
        public static bool IsBuffer(Type t) => Resources.TryGetValue(t, out var meta) && meta.IsBuffer;
        public static bool IsTexture(Type t) => Resources.TryGetValue(t, out var meta) && meta.IsTexture;
        public static bool IsReadWrite(Type t) => Resources.TryGetValue(t, out var meta) && meta.IsReadWrite;
        public static Type GetPayloadType(Type t) => Resources.TryGetValue(t, out var meta) ? meta.PayloadType : t;
        
        // Name sanitization
        private static readonly Regex SafeNameRegex = new Regex(@"[^a-zA-Z0-9_]", RegexOptions.Compiled);

        public static string GetSafeHLSLName(string rawName)
        {
            if (string.IsNullOrEmpty(rawName)) return "_Var";
            string safeName = SafeNameRegex.Replace(rawName, "_");
            return char.IsDigit(safeName[0]) ? "_" + safeName : safeName;
        }

        public static string GetHLSLDeclarationFromCSType(Type t, string safeName)
        {
            if (Resources.TryGetValue(t, out var meta))
            {
                string payloadStr = GetStringFromCSType(meta.PayloadType);
                return string.Format(meta.DeclarationTemplate, payloadStr, safeName);
            }
            return $"{GetStringFromCSType(t)} {safeName};";
        }
        
        public static string FormatValueHLSL(object value, Type type)
        {
            // Fallback value
            if (value == null) return "0";
            
            // Value type switch
            return value switch
            {
                float f => f.ToString("G", CultureInfo.InvariantCulture),
                int i => i.ToString("G", CultureInfo.InvariantCulture),
                bool b => b.ToString().ToLower(),
                float2 v => $"float2({v.x.ToString("G")}, {v.y.ToString("G")})",
                float3 v => $"float3({v.x.ToString("G")}, {v.y.ToString("G")}, {v.z.ToString("G")})",
                float4 v => $"float4({v.x.ToString("G")}, {v.y.ToString("G")}, {v.z.ToString("G")}, {v.w.ToString("G")})",
                int2 v => $"int2({v.x.ToString("G")}, {v.y.ToString("G")})",
                int3 v => $"int3({v.x.ToString("G")}, {v.y.ToString("G")}, {v.z.ToString("G")})",
                int4 v => $"int4({v.x.ToString("G")}, {v.y.ToString("G")}, {v.z.ToString("G")}, {v.w.ToString("G")})",
                _ => "0"
            };
        }

        public static string GetStringFromCSType(Type t)
        {
            if (t == typeof(float)) return "float";
            if (t == typeof(float2)) return "float2";
            if (t == typeof(float3)) return "float3";
            if (t == typeof(float4)) return "float4";
            if (t == typeof(int)) return "int";
            if (t == typeof(int2)) return "int2";
            if (t == typeof(int3)) return "int3";
            if (t == typeof(int4)) return "int4";
            if (t == typeof(bool)) return "bool";
            return "float";
        }
    }
}