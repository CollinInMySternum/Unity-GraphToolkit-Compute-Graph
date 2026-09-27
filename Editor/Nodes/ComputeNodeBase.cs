using System;
using System.Collections.Generic;
using System.Linq;
using Unity.GraphToolkit.Editor;
using Unity.Mathematics;
using UnityEngine;

namespace Editor.Nodes
{
    [Serializable]
    [Node("", "", "", StylePath)]
    [UseWithGraph(typeof(ComputeGraph))]
    [DataTypeStyleMapper()]
    public abstract class ComputeNodeBase : Node
    {
        public const string StylePath = "Packages/com.sternum.computegraph/Editor/Styles/ComputeNodeStylesMaster.uss";
        
        [NonSerialized] private string _cachedVarName;

        public void ResetCompilationState()
        {
            _cachedVarName = null;
        }

        public string GetOrEmitHLSL(ComputeGraphCompiler compiler, string outputPortName)
        {
            if (string.IsNullOrEmpty(_cachedVarName))
            {
                _cachedVarName = compiler.GetUniqueVarName(GetType().Name.Replace("Node", ""));
                EmitHLSL(compiler, _cachedVarName);
            }

            return FormatOutputVariable(_cachedVarName, outputPortName);
        }

        protected abstract void EmitHLSL(ComputeGraphCompiler compiler, string outputVar);

        protected virtual string FormatOutputVariable(string baseVarName, string outputPortName)
        {
            return baseVarName;
        }

        protected string EvaluateInput(ComputeGraphCompiler compiler, string portName, string fallbackValue = "0.0")
        {
            var port = GetInputPortByName(portName);
            if (port == null) return fallbackValue;
            
            var connectedPorts = new List<IPort>();
            port.GetConnectedPorts(connectedPorts);
            var connectedPort = connectedPorts.FirstOrDefault();

            // Handle inline UI Values
            if (connectedPort == null)
            {
                Type type = port.DataType;
                
                if (type == typeof(float) && port.TryGetValue<float>(out var v1)) return ComputeGraphTypes.FormatValueHLSL(v1, type);
                if (type == typeof(int) && port.TryGetValue<int>(out var v2)) return ComputeGraphTypes.FormatValueHLSL(v2, type);
                if (type == typeof(float2) && port.TryGetValue<float2>(out var v3)) return ComputeGraphTypes.FormatValueHLSL(v3, type);
                if (type == typeof(float3) && port.TryGetValue<float3>(out var v4)) return ComputeGraphTypes.FormatValueHLSL(v4, type);
                if (type == typeof(float4) && port.TryGetValue<float4>(out var v5)) return ComputeGraphTypes.FormatValueHLSL(v5, type);
                if (type == typeof(int2) && port.TryGetValue<int2>(out var v6)) return ComputeGraphTypes.FormatValueHLSL(v6, type);
                if (type == typeof(int3) && port.TryGetValue<int3>(out var v7)) return ComputeGraphTypes.FormatValueHLSL(v7, type);
                if (type == typeof(int4) && port.TryGetValue<int4>(out var v8)) return ComputeGraphTypes.FormatValueHLSL(v8, type);
                if (type == typeof(bool) && port.TryGetValue<bool>(out var v9)) return ComputeGraphTypes.FormatValueHLSL(v9, type);
            }
            
            // Standard compute nodes
            if (connectedPort.GetNode() is ComputeNodeBase connectedNode)
            {
                return connectedNode.GetOrEmitHLSL(compiler, connectedPort.Name);
            }

            // GTF Variables/Uniforms
            if (connectedPort.GetNode() is IVariableNode variableNode)
            {
                Type dataType = variableNode.Variable.DataType;
                string safeName = ComputeGraphTypes.GetSafeHLSLName(variableNode.Variable.Name);
        
                if (!compiler.RegisteredUniforms.Contains(safeName))
                {
                    compiler.RegisteredUniforms.Add(safeName);
                    string hlslDecl = ComputeGraphTypes.GetHLSLDeclarationFromCSType(dataType, safeName);
                    compiler.Declarations.AppendLine(hlslDecl);
                }

                return safeName;
            }
            
            // GTF Constants/Literals
            if (connectedPort.GetNode() is IConstantNode constantNode)
            {
                Type type = connectedPort.DataType;
                
                if (type == typeof(float) && constantNode.TryGetValue<float>(out var v1)) return ComputeGraphTypes.FormatValueHLSL(v1, type);
                if (type == typeof(int) && constantNode.TryGetValue<int>(out var v2)) return ComputeGraphTypes.FormatValueHLSL(v2, type);
                if (type == typeof(float2) && constantNode.TryGetValue<float2>(out var v3)) return ComputeGraphTypes.FormatValueHLSL(v3, type);
                if (type == typeof(float3) && constantNode.TryGetValue<float3>(out var v4)) return ComputeGraphTypes.FormatValueHLSL(v4, type);
                if (type == typeof(float4) && constantNode.TryGetValue<float4>(out var v5)) return ComputeGraphTypes.FormatValueHLSL(v5, type);
                if (type == typeof(int2) && constantNode.TryGetValue<int2>(out var v6)) return ComputeGraphTypes.FormatValueHLSL(v6, type);
                if (type == typeof(int3) && constantNode.TryGetValue<int3>(out var v7)) return ComputeGraphTypes.FormatValueHLSL(v7, type);
                if (type == typeof(int4) && constantNode.TryGetValue<int4>(out var v8)) return ComputeGraphTypes.FormatValueHLSL(v8, type);
                if (type == typeof(bool) && constantNode.TryGetValue<bool>(out var v9)) return ComputeGraphTypes.FormatValueHLSL(v9, type);
            }

            return fallbackValue;
        }
    }
}