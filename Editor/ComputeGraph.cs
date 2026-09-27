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
    [Graph(AssetExtension, options: GraphOptions.DisableAutoInclusionOfNodesFromGraphAssembly)]
    [Serializable]
    public class ComputeGraph : Graph
    {
        [SerializeField] private string guid = Guid.NewGuid().ToString();
        [SerializeField] private ComputeShader computeShader;
        
        public const string AssetExtension = "cg";

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

            var writeNode = GetNodes().OfType<WriteBuffer>().FirstOrDefault();

            if (writeNode == null)
            {
                Debug.LogError("Compilation Failed: No Output Node found on graph.");
                return;
            }

            foreach (var node in GetNodes().OfType<ComputeNodeBase>())
            {
                node.ResetCompilationState();
            }

            writeNode.GetOrEmitHLSL(compiler, "Result");
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
            
            // Resolve types
            foreach (var node in GetNodes().OfType<ComputeNodeWildcardBase>())
            {
                node.ResolveType();
            }
            
            // End fake transaction
            UndoEndRecordGraph();
        }

        public override bool IsConnectionAllowed(IPort output, IPort input)
        {
            return !(output.DataType == typeof(Untyped) && input.DataType == typeof(Untyped)) &&
                   !(output.DataType == typeof(Untyped) && input.DataType != typeof(Untyped));
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