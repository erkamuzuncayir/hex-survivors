using System;

[Serializable]
public class FloatReference
{
#region Fields
    public bool useConstant;
    public float constantValue;
    public SharedFloat variable;
#endregion

#region Implementations
    public float Value =>
        useConstant ? constantValue : 
            variable.value;

#endregion
}
