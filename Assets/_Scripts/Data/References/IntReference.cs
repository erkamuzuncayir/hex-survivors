using System;
using _Scripts.Data.Type;

namespace _Scripts.Data.References
{
    [Serializable]
    public class IntReference
    {
        public bool UseConstant;
        public int ConstantValue;
        public IntSO Variable;

        public int Value =>
            UseConstant ? ConstantValue : Variable.Value;
    }
}