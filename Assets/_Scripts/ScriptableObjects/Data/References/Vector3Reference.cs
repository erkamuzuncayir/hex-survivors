using System;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class Vector3Reference
{
    public bool UseConstant;
    public Vector3 ConstantValue;
    public Vector3SO Variable;

    public UnityEngine.Vector3 Value =>
        UseConstant ? ConstantValue : 
            Variable.value;
}