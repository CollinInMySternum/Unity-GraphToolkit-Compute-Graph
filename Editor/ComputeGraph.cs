using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Editor;
using Editor.Nodes;
using Editor.Nodes.Buffers;
using Unity.GraphToolkit.Editor;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [Graph(AssetExtension)]
    [Serializable]
    public class ComputeGraph : Graph
    {
        [SerializeField] private string guid = Guid.NewGuid().ToString();
        [SerializeField] private ComputeShader computeShader;
        
        public const string AssetExtension = "cgraph";

        [MenuItem("Assets/Create/Compute Graph", false)]
        static void CreateAssetFile()
        {
            GraphDatabase.PromptInProjectBrowserToCreateNewAsset<ComputeGraph>();
        }
        
        public static ComputeGraph ActiveGraph { get; private set; }
        
        public override void OnEnable()
        {
            base.OnEnable();
            ActiveGraph = this;
        }

        public override void OnDisable()
        {
            base.OnDisable();
            if (ActiveGraph == this) ActiveGraph = null; 
        }

        public void CompileToHLSL()
        {
            var compiler = new ComputeGraphCompiler();

            var outputNodes = GetNodes().OfType<IComputeNodeOutput>().Cast<ComputeNodeBase>().ToList();

            if (outputNodes.Count == 0)
            {
                Debug.LogError("Compilation Failed: No Output Nodes found on graph.");
                return;
            }

            foreach (var node in GetNodes().OfType<ComputeNodeBase>())
            {
                node.ResetCompilationState();
            }

            foreach (var outputNode in outputNodes)
            {
                outputNode.GetOrEmitHLSL(compiler, "Result");
            }

            string finalCode = compiler.GetCompiledShader();
            
            CreateComputeAsset(finalCode);
            
            Debug.Log($"Compilation Successful\n\n {finalCode}");
        }

        public override void OnGraphChanged(GraphLogger graphLogger)
        {
            EditorApplication.update -= PerformDeferredRefresh;
            EditorApplication.update += PerformDeferredRefresh;

            base.OnGraphChanged(graphLogger);
        }
        
        // Hacky - forces override by spoofing user interaction
        public void PerformDeferredRefresh()
        {
            EditorApplication.update -= PerformDeferredRefresh;
            
            if (this == null) return;
            
            // Submit fake transaction
            UndoBeginRecordGraph("Resolve wildcard types");

            var wildcardNodes = GetNodes().OfType<ComputeNodeWildcardBase>().ToList();
            
            // Resolve types
            foreach (var node in wildcardNodes)
            {
                node.CacheWildcardState();
            }
            
            // Rebuild nodes
            foreach (var node in wildcardNodes)
            {
                node.DefineNode();
            }
            
            // End fake transaction
            UndoEndRecordGraph();
        }

        public override bool IsConnectionAllowed(IPort output, IPort input)
        {
            if (output.DataType == typeof(Untyped) && input.DataType == typeof(Untyped)) return false;
            if (output.DataType == typeof(Untyped) && input.DataType != typeof(Untyped)) return false;

            // Wildcard rules
            if (input.GetNode() is ComputeNodeWildcardBase wildcardNode)
            {
                // Check input port connected is one of the wildcard ports
                if (wildcardNode.wildcardPorts.Contains(input.Name))
                {
                    // Check if the type is accepted
                    return wildcardNode.IsValidWildcardType(output.DataType);
                }
                
                // Inferred port handling
                // If a port type is inferred or resolved dynamically, and is Untyped, then we should not allow inputs.
                if (wildcardNode.GetPortDefinition(input.Name, out var portDefinition))
                {
                    if (portDefinition.DynamicTypeResolver != null && input.DataType == typeof(Untyped)) return false;
                }
            }

            // Standard strict type matching
            if (input.DataType != typeof(Untyped) && output.DataType != typeof(Untyped))
            {
                return input.DataType == output.DataType;
            }

            return true;
        }

        public void CreateComputeAsset(string source)
        {
            string folderPath = "Assets/ComputeGraph/.generated";

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string assetPath = $"{folderPath}/{guid}.compute";
            
            File.WriteAllText(assetPath, source);
            AssetDatabase.ImportAsset(assetPath);

            computeShader = AssetDatabase.LoadAssetAtPath<ComputeShader>(assetPath);
            
            AssetDatabase.SaveAssets();
        }
    }
}