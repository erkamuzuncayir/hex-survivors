using System;
using _Scripts.Data.Type;
using UnityEngine;

namespace _Scripts.Data.References
{
    [Serializable]
    public class Vector3Reference
    {
        public bool UseConstant;
        public Vector3 ConstantValue;
        public Vector3SO Variable;

        public Vector3 Value =>
            UseConstant ? ConstantValue : Variable.Value;
    }
}