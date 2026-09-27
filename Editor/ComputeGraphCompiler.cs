using System.Collections.Generic;
using System.Text;

namespace Editor
{
    public class ComputeGraphCompiler
    {
        public StringBuilder Declarations = new StringBuilder();
        public StringBuilder Functions = new StringBuilder();
        public StringBuilder Body = new StringBuilder();

        public HashSet<string> RegisteredUniforms = new HashSet<string>();
        public HashSet<string> RegisteredFunctions = new HashSet<string>();

        private int varCounter = 0;

        public string GetUniqueVarName(string prefix = "var")
        {
            return $"{prefix}_{varCounter++}";
        }

        public string GetCompiledShader()
        {
            return $"#pragma kernel CSMain\n" +
                   $"{Declarations}\n\n" +
                   $"{Functions}\n\n" +
                   $"[numthreads(8,8,1)]\n" +
                   $"void CSMain(uint3 id : SV_DispatchThreadID)\n" +
                   $"{{\n" +
                   $"{Body}\n" +
                   $"}}";
        }
    }
}