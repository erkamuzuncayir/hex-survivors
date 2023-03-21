using System;
using UnityEngine;

[Serializable]
public class Vector3Reference
{
#region Fields
    public bool useConstant;
    public Vector3 constantValue;
    public SharedVector3 variable;
#endregion

#region Implementations
    public Vector3 Value =>
        useConstant ? constantValue : 
            variable.value;

#endregion
}