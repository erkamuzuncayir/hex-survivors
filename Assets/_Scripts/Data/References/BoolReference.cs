using System;
using _Scripts.Data.Type;

namespace _Scripts.Data.References
{
    [Serializable]
    public class BoolReference
    {
        public bool UseConstant;
        public bool ConstantValue;
        public BoolSO Variable;

        public bool Value =>
            UseConstant ? ConstantValue : Variable.Value;
    }
}