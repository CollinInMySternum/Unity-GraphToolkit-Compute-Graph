# Unity Compute Graph Experiments

A work-in-progress visual graph editor for curating Compute Shaders in Unity using the Graph Toolkit framework.

<img width="963" height="680" alt="Screenshot 2026-09-27 182405" src="https://github.com/user-attachments/assets/7a55fd86-2d82-4218-854b-83f8f1cb56be" />

After experimenting a bit with Unity, I found I needed a graphing system that replicates the shader graph, but with more functionality and types, as well as the ability to dispatch as a compute shader.

## Features

- Visual Compute Programming similar to the Shader Graph
- Typical data types (float/floatX, int/intX, bool)
- RWBuffers/Buffers with float and integer payloads
- Basic Wildcard Arithmetic, Operators, and Trigonometry
- Dynamic HLSL "compilation" to a .compute asset

## Architecture

- Coming soon, still working things out and experimenting :)
