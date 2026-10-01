using Editor;
using UnityEngine;

public struct ComputeGraphDispatcher
{
    public ComputeShader Shader { get; }
    public int KernelIndex { get; }

    public ComputeGraphDispatcher(ComputeShader computeShader, string kernelName = "CSMain")
    {
        Shader = computeShader;
        KernelIndex = 0;

        if (Shader == null)
        {
            Debug.LogError("[ComputeGraph] Dispatcher received a null ComputeShader asset.");
            return;
        }

        KernelIndex = Shader.FindKernel(kernelName);
    }
    
    // Data binding
    public void SetFloat(string rawName, float value)
    {
        Shader.SetFloat(ComputeGraphTypes.GetSafeHLSLName(rawName), value);
    }

    public void SetInt(string rawName, int value)
    {
        Shader.SetInt(ComputeGraphTypes.GetSafeHLSLName(rawName), value);
    }

    public void SetBool(string rawName, bool value)
    {
        Shader.SetBool(ComputeGraphTypes.GetSafeHLSLName(rawName), value);
    }

    public void SetVector(string rawName, Vector4 vector)
    {
        Shader.SetVector(ComputeGraphTypes.GetSafeHLSLName(rawName), vector);
    }

    public void SetTexture(string rawName, Texture texture)
    {
        Shader.SetTexture(KernelIndex, ComputeGraphTypes.GetSafeHLSLName(rawName), texture);
    }

    public void SetBuffer(string rawName, ComputeBuffer buffer)
    {
        Shader.SetBuffer(KernelIndex, ComputeGraphTypes.GetSafeHLSLName(rawName), buffer);
    }
    
    // Dispatching
    public void Dispatch(int threadGroupsX, int threadGroupsY = 1, int threadGroupsZ = 1)
    {
        if (Shader != null)
        {
            Shader.Dispatch(KernelIndex, threadGroupsX, threadGroupsY, threadGroupsZ);
        }
    }
}