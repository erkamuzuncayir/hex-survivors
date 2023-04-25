using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddObjectToRuntimeSet : MonoBehaviour
{
#region Fields
    public GameObjectRuntimeSet gameObject_runtimeSet;
#endregion

#region Unity API
    void OnEnable()
    {
        gameObject_runtimeSet.AddToList( gameObject );
    }
    void OnDisable()
    {
        gameObject_runtimeSet.RemoveFromList( gameObject );
    }
#endregion
}
