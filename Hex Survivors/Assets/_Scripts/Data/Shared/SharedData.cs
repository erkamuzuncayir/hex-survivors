
using UnityEngine;

public class SharedData < TSharedDataType > : ScriptableObject
{
    #region Fields
    public TSharedDataType value;
    #endregion

    #region Editor Only
#if UNITY_EDITOR
#endif
    #endregion
}