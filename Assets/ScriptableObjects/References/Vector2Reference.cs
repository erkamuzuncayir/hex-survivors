using System;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class Vector2Reference
{
    public bool UseConstant;
    public Vector2 ConstantValue;
    public Vector2SO Variable;

    public Vector2 Value =>
        UseConstant ? ConstantValue : 
            Variable.value;

}