using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

/// <summary>
/// This class inherits scriptable object to create customized event layer for project.
/// </summary>
[CreateAssetMenu(fileName = "GameObject Event", menuName = "Events/GameObject Event")]
public class GameObjectEvent : DescriptionBaseSO
{
    List<GameObjectEventListener> _eventListenerList = new();
    public bool hasAnyDependent;
    [ShowIf("hasAnyDependent")] public List<GameObjectEvent> eventsDependOnThis;
    public void Raise(GameObject param)
    {
        for (int i = _eventListenerList.Count - 1; i >= 0; i--)
            _eventListenerList[i].OnEventRaised(param);
    }

    [ShowIf("hasAnyDependent")]
    public void RaiseSelfAndDependents(GameObject param)
    {
        Raise(param);

        if (hasAnyDependent && eventsDependOnThis != null)
            for (var i = 0; i < eventsDependOnThis.Count; i++)
                eventsDependOnThis[i].Raise(param);
    }

    public void RegisterListener(GameObjectEventListener listener)
    {
        _eventListenerList.Add(listener);
    }

    public void UnregisterListener(GameObjectEventListener listener)
    {
        _eventListenerList.Remove(listener);
    }
}