using System;
using UnityEngine;

[Serializable]
public class Vector2Reference
{
#region Fields
    public bool useConstant;
    public Vector2 constantValue;
    public SharedVector2 variable;
#endregion

#region Implementations
    public Vector2 Value =>
        useConstant ? constantValue : 
            variable.value;

#endregion
}