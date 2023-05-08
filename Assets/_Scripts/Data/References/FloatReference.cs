using System;
using _Scripts.Data.Type;

namespace _Scripts.Data.References
{
    [Serializable]
    public class FloatReference
    {
        public bool UseConstant;
        public float ConstantValue;
        public FloatSO Variable;

        public float Value =>
            UseConstant ? ConstantValue : Variable.Value;
    }
}