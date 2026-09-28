using System;
using System.Collections.Generic;
using Unity.GraphToolkit.Editor;
using Unity.Mathematics;
using UnityEngine;

namespace Editor.Nodes.Vector
{
    [Serializable]
    [Node("Math/Float2", "", "Break Float2", StylePath)]
    public class BreakFloat2 : ComputeNodeBase
    {
        protected override IEnumerable<PortDefinition> DefinedPorts => new[]
        {
            PortDefinition.Input("In", type: typeof(float2)),
            
            PortDefinition.Output("X", type: typeof(float)),
            PortDefinition.Output("Y", type: typeof(float)),
        };

        protected override void EmitHLSL(ComputeGraphCompiler compiler, string outputVar)
        {
            string valIn = EvaluateInput(compiler, "In", "float2(0.0, 0.0)");
                
            compiler.Body.AppendLine($"    float2 {outputVar} = {valIn};");
        }
        
        protected override string FormatOutputVariable(string baseVarName, string outPortName)
        {
            switch (outPortName)
            {
                case "X": return $"{baseVarName}.x";
                case "Y": return $"{baseVarName}.y";
                default: return baseVarName;
            }
        }
    }
    
    [Serializable]
    [Node("Math/Float3", "", "Break Float3", StylePath)]
    public class BreakFloat3 : ComputeNodeBase
    {
        protected override IEnumerable<PortDefinition> DefinedPorts => new[]
        {
            PortDefinition.Input("In", type: typeof(float3)),
            
            PortDefinition.Output("X", type: typeof(float)),
            PortDefinition.Output("Y", type: typeof(float)),
            PortDefinition.Output("Z", type: typeof(float))
        };

        protected override void EmitHLSL(ComputeGraphCompiler compiler, string outputVar)
        {
            string valIn = EvaluateInput(compiler, "In", "float3(0.0, 0.0, 0.0)");
                
            compiler.Body.AppendLine($"    float3 {outputVar} = {valIn};");
        }
        
        protected override string FormatOutputVariable(string baseVarName, string outPortName)
        {
            switch (outPortName)
            {
                case "X": return $"{baseVarName}.x";
                case "Y": return $"{baseVarName}.y";
                case "Z": return $"{baseVarName}.z";
                default: return baseVarName;
            }
        }
    }
    
    [Serializable]
    [Node("Math/Float4", "", "Break Float4", StylePath)]
    public class BreakFloat4 : ComputeNodeBase
    {
        protected override IEnumerable<PortDefinition> DefinedPorts => new[]
        {
            PortDefinition.Input("In", type: typeof(float4)),
            
            PortDefinition.Output("X", type: typeof(float)),
            PortDefinition.Output("Y", type: typeof(float)),
            PortDefinition.Output("Z", type: typeof(float)),
            PortDefinition.Output("W", type: typeof(float))
        };

        protected override void EmitHLSL(ComputeGraphCompiler compiler, string outputVar)
        {
            string valIn = EvaluateInput(compiler, "In", "float4(0.0, 0.0, 0.0, 0.0)");
                
            compiler.Body.AppendLine($"    float4 {outputVar} = {valIn};");
        }
        
        protected override string FormatOutputVariable(string baseVarName, string outPortName)
        {
            switch (outPortName)
            {
                case "X": return $"{baseVarName}.x";
                case "Y": return $"{baseVarName}.y";
                case "Z": return $"{baseVarName}.z";
                case "W": return $"{baseVarName}.w";
                default: return baseVarName;
            }
        }
    }
    
    [Serializable]
    [Node("Math/Int2", "", "Break Int2", StylePath)]
    public class BreakInt2 : ComputeNodeBase
    {
        protected override IEnumerable<PortDefinition> DefinedPorts => new[]
        {
            PortDefinition.Input("In", type: typeof(int2)),
            
            PortDefinition.Output("X", type: typeof(int)),
            PortDefinition.Output("Y", type: typeof(int)),
        };

        protected override void EmitHLSL(ComputeGraphCompiler compiler, string outputVar)
        {
            string valIn = EvaluateInput(compiler, "In", "int2(0, 0)");
                
            compiler.Body.AppendLine($"    int2 {outputVar} = {valIn};");
        }
        
        protected override string FormatOutputVariable(string baseVarName, string outPortName)
        {
            switch (outPortName)
            {
                case "X": return $"{baseVarName}.x";
                case "Y": return $"{baseVarName}.y";
                default: return baseVarName;
            }
        }
    }
    
    [Serializable]
    [Node("Math/Int3", "", "Break Int3", StylePath)]
    public class BreakInt3 : ComputeNodeBase
    {
        protected override IEnumerable<PortDefinition> DefinedPorts => new[]
        {
            PortDefinition.Input("In", type: typeof(int3)),
            
            PortDefinition.Output("X", type: typeof(int)),
            PortDefinition.Output("Y", type: typeof(int)),
            PortDefinition.Output("Z", type: typeof(int)),
        };

        protected override void EmitHLSL(ComputeGraphCompiler compiler, string outputVar)
        {
            string valIn = EvaluateInput(compiler, "In", "int3(0, 0, 0)");
                
            compiler.Body.AppendLine($"    int3 {outputVar} = {valIn};");
        }
        
        protected override string FormatOutputVariable(string baseVarName, string outPortName)
        {
            switch (outPortName)
            {
                case "X": return $"{baseVarName}.x";
                case "Y": return $"{baseVarName}.y";
                case "Z": return $"{baseVarName}.z";
                default: return baseVarName;
            }
        }
    }
    
    [Serializable]
    [Node("Math/Int4", "", "Break Int4", StylePath)]
    public class BreakInt4 : ComputeNodeBase
    {
        protected override IEnumerable<PortDefinition> DefinedPorts => new[]
        {
            PortDefinition.Input("In", type: typeof(int4)),
            
            PortDefinition.Output("X", type: typeof(int)),
            PortDefinition.Output("Y", type: typeof(int)),
            PortDefinition.Output("Z", type: typeof(int)),
            PortDefinition.Output("W", type: typeof(int))
        };

        protected override void EmitHLSL(ComputeGraphCompiler compiler, string outputVar)
        {
            string valIn = EvaluateInput(compiler, "In", "int4(0, 0, 0, 0)");
                
            compiler.Body.AppendLine($"    int4 {outputVar} = {valIn};");
        }

        protected override string FormatOutputVariable(string baseVarName, string outPortName)
        {
            switch (outPortName)
            {
                case "X": return $"{baseVarName}.x";
                case "Y": return $"{baseVarName}.y";
                case "Z": return $"{baseVarName}.z";
                case "W": return $"{baseVarName}.w";
                default: return baseVarName;
            }
        }
    }
}