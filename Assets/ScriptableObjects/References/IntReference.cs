using System;

[Serializable]
public class IntReference
{
#region Fields
    public bool useConstant;
    public int constantValue;
    public IntSO variable;
#endregion

#region Implementations
    public int Value =>
        useConstant ? constantValue : 
            variable.value;

#endregion
}
