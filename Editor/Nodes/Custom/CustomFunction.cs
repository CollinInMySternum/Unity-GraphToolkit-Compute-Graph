// using System;
// using Unity.GraphToolkit.Editor;
// using System.Collections.Generic;
//
// namespace Editor.Nodes.Custom
// {
//     [Serializable]
//     [Node("Custom/HLSL Function", "", "Custom Function", StylePath)]
//     public class CustomFunction : ComputeNodeBase
//     {
//         // Option names for node values
//         private const string k_FunctionName = "FunctionName";
//         private const string k_ReturnType = "ReturnType";
//         private const string k_Signature = "Signature";
//         private const string k_CodeBody = "CodeBody";
//         
//         // Setting up options
//         protected override void OnDefineOptions(IOptionDefinitionContext context)
//         {
//             context.AddOption<string>(k_FunctionName)
//                 .WithDisplayName("Function Name")
//                 .WithDefaultValue("Multiply A * B");
//             
//             context.AddOption<string>(k_ReturnType)
//                 .WithDisplayName("Return Type")
//                 .WithDefaultValue("float");
//             
//             context.AddOption<string>(k_Signature)
//                 .WithDisplayName("In/Out Arguments")
//                 .WithDefaultValue("in float a, in float b")
//                 .Delayed();
//
//             context.AddOption<string>(k_CodeBody)
//                 .WithDisplayName("Code Body")
//                 .WithDefaultValue("return a * b;")
//                 .AsTextArea(3, 512);
//         }
//
//         protected override void OnDefinePorts(IPortDefinitionContext context)
//         {
//             GetNodeOptionByName(k_ReturnType).TryGetValue<string>(out var returnType);
//             GetNodeOptionByName(k_Signature).TryGetValue<string>(out var signature);
//
//             if (returnType.Trim().ToLower() != "void")
//             {
//                 context.AddOutputPort("Result").WithDataType(
//                     ComputeGraphTypes.GetCSharpType(ComputeGraphTypes.GetHLSLTypeFromString(returnType))).Build();
//             }
//
//             if (!string.IsNullOrWhiteSpace(signature))
//             {
//                 string[] args = signature.Split(',');
//
//                 foreach (string arg in args)
//                 {
//                     string[] tokens = arg.Trim().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
//
//                     if (tokens.Length >= 3)
//                     {
//                         string inOut = tokens[0];
//                         string hlslType = tokens[1];
//                         string portName = tokens[2];
//
//                         Type csType = ComputeGraphTypes.GetCSharpType(ComputeGraphTypes.GetHLSLTypeFromString(hlslType));
//
//                         if (inOut.Contains("in"))
//                         {
//                             context.AddInputPort(portName).WithDataType(csType).Build();
//                         }
//
//                         if (inOut.Contains("out"))
//                         {
//                             context.AddOutputPort(portName).WithDataType(csType).Build();
//                         }
//                     }
//                 }
//             }
//         }
//
//         protected override string FormatOutputVariable(string baseVarName, string outputPortName)
//         {
//             if (outputPortName == "Result")
//             {
//                 return baseVarName;
//             }
//
//             return $"{baseVarName}_{outputPortName}";
//         }
//
//         protected override void EmitHLSL(ComputeGraphCompiler compiler, string outputVar)
//         {
//             GetNodeOptionByName(k_FunctionName).TryGetValue<string>(out var funcName);
//             GetNodeOptionByName(k_CodeBody).TryGetValue<string>(out var codeBody);
//             GetNodeOptionByName(k_Signature).TryGetValue<string>(out var signature);
//             GetNodeOptionByName(k_ReturnType).TryGetValue<string>(out var returnType);
//             
//             // Emit function definition
//             if (!compiler.RegisteredFunctions.Contains(funcName))
//             {
//                 compiler.RegisteredFunctions.Add(funcName);
//                 
//                 compiler.Functions.AppendLine($"{returnType} {funcName}({signature})");
//                 compiler.Functions.AppendLine("{");
//                 compiler.Functions.AppendLine($"    {codeBody}");
//                 compiler.Functions.AppendLine("}");
//                 compiler.Functions.AppendLine();
//             }
//
//             var evaluatedArgs = new List<string>();
//             string[] args = signature.Split(',');
//             
//             foreach (string arg in args)
//             {
//                 string[] tokens = arg.Trim().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
//                 if (tokens.Length >= 3)
//                 {
//                     string inOut = tokens[0];
//                     string hlslType = tokens[1];
//                     string portName = tokens[2];
//
//                     if (inOut == "in")
//                     {
//                         evaluatedArgs.Add(EvaluateInput(compiler, portName));
//                     }
//                     
//                     if (inOut == "out" || inOut == "inout")
//                     {
//                         string localOutVar = $"{outputVar}_{portName}";
//
//                         compiler.Body.AppendLine($"    {hlslType} {localOutVar};");
//
//                         if (inOut == "inout")
//                         {
//                             string inValue = EvaluateInput(compiler, portName);
//                             compiler.Body.AppendLine($"    {localOutVar} = {inValue};");
//                         }
//                         
//                         evaluatedArgs.Add(localOutVar);
//                     }
//                 }
//             }
//
//             string callArgs = string.Join(", ", evaluatedArgs);
//
//             if (returnType.Trim().ToLower() == "void")
//             {
//                 compiler.Body.AppendLine($"    {funcName}({callArgs});");
//             }
//             else
//             {
//                 compiler.Body.AppendLine($"    {returnType} {outputVar} = {funcName}({callArgs});");
//             }
//         }
//     }
// }