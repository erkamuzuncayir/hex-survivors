using System;
using UnityEngine.Serialization;

[Serializable]
public class BoolReference
{
#region Fields
    public bool useConstant; 
    public bool constantValue;
    public BoolSO variable;
#endregion

#region Implementations
    public bool Value =>
        useConstant ? constantValue : 
            variable.value;

#endregion
}
