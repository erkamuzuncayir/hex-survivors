using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddObjectToRuntimeSet : MonoBehaviour
{
    public GameObjectRuntimeSet GameObjectRuntimeSet;
    
    void OnEnable()
    {
        GameObjectRuntimeSet.AddToList(gameObject);
    }
    void OnDisable()
    {
        GameObjectRuntimeSet.RemoveFromList(gameObject);
    }
}