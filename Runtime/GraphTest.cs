using UnityEngine;

[RequireComponent(typeof(Camera))]
public class GraphTest : MonoBehaviour
{
    [Header("Compute Graph")]
    public ComputeShader computeShader;
    public int resolution = 1024;

    [Header("Debug")]
    public RenderTexture renderTexture; 
    private ComputeGraphDispatcher dispatcher;

    void Start()
    {
        renderTexture = new RenderTexture(resolution, resolution, 0, RenderTextureFormat.ARGBFloat);
        renderTexture.enableRandomWrite = true;
        renderTexture.filterMode = FilterMode.Point;
        renderTexture.Create();

        if (computeShader != null)
        {
            dispatcher = new ComputeGraphDispatcher(computeShader);
        }
    }

    void Update()
    {
        if (computeShader == null || renderTexture == null)
        {
            return;
        }

        dispatcher.SetTexture("ResultTex", renderTexture);
        dispatcher.SetFloat("Resolution", resolution);

        int threadGroups = Mathf.CeilToInt(resolution / 8f);
        dispatcher.Dispatch(threadGroups, threadGroups, 1);
    }

    void OnDestroy()
    {
        if (renderTexture != null)
        {
            renderTexture.Release();
        }
    }
}