using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

/// <summary>
/// This class inherits scriptable object to create customized event layer for project.
/// </summary>
[CreateAssetMenu(fileName = "Vector2 Event", menuName = "Events/Vector2 Event")]
public class Vector2Event : DescriptionBaseSO
{
    #region Fields
    List< Vector2EventListener > _eventListenerList = new();
    public bool hasAnyDependent;
    [ ShowIf( "hasAnyDependent" ) ]
    public List< Vector2Event > eventsDependOnThis;
    #endregion
    
    #region API
    public void Raise(Vector2 param)
    {
        for (int i = _eventListenerList.Count - 1; i >= 0; i--)
            _eventListenerList[i].OnEventRaised(param);
    }
    
    [ShowIf("hasAnyDependent")]
    public void RaiseSelfAndDependents(Vector2 param)
    {
        Raise(param);

        if(hasAnyDependent && eventsDependOnThis != null)
            for(var i = 0; i < eventsDependOnThis.Count; i++)
                eventsDependOnThis[i].Raise(param);
    }
    
    public void RegisterListener(Vector2EventListener listener)
    { 
        _eventListenerList.Add(listener); 
    }

    public void UnregisterListener(Vector2EventListener listener)
    { 
        _eventListenerList.Remove(listener); 
    }
    #endregion

    #region Editor Only
#if UNITY_EDITOR
#endif
    #endregion
}