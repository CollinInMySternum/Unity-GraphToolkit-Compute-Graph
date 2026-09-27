using System;
using Unity.GraphToolkit.Editor;

namespace Editor.Nodes.Conversions
{
    [Serializable]
    [Node("Conversions", "", "Convert Bool to Integer", StylePath)]
    public class ConvertBoolToInt : ConvertXToY
    {
        protected override Type workingTypeX => typeof(bool);
        protected override Type workingTypeY => typeof(int);
    }
    
    [Serializable]
    [Node("Conversions", "", "Convert Integer to Bool", StylePath)]
    public class ConvertIntToBool : ConvertXToY
    {
        protected override Type workingTypeX => typeof(int);
        protected override Type workingTypeY => typeof(bool);
    }
}