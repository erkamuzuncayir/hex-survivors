using System;
using _Scripts.Data.Type;
using UnityEngine;

namespace _Scripts.Data.References
{
    [Serializable]
    public class Vector2Reference
    {
        public bool UseConstant;
        public Vector2 ConstantValue;
        public Vector2SO Variable;

        public Vector2 Value =>
            UseConstant ? ConstantValue : Variable.Value;
    }
}