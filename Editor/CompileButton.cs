using Unity.GraphToolkit.Editor;
using UnityEditor.Toolbars;
using UnityEngine;

namespace Editor
{
    [GraphToolbarElement("Compile", typeof(ComputeGraph), order: 0)]
    public class ComputeGraphCompileButton : EditorToolbarButton
    {
        public ComputeGraphCompileButton()
        {
            text = "Compile HLSL";
            tooltip = "Compiles the active graph to HLSL";

            clicked += () =>
            {
                if (ComputeGraph.ActiveGraph != null)
                {
                    ComputeGraph.ActiveGraph.CompileToHLSL();
                }
                else
                {
                    Debug.LogWarning("No active Compute Graph is loaded to compile.");
                }
            };

            style.backgroundColor = new Color(0.1f, 0.8f, 0.3f, 1.0f);
            style.color = Color.white;
            style.unityFontStyleAndWeight = FontStyle.Bold;
            style.minWidth = 120;
        }
    }
}