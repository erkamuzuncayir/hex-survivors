using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace _Scripts.Events
{
    /// <summary>
    ///     This class inherits scriptable object to create customized event layer for project.
    /// </summary>
    [CreateAssetMenu(fileName = "Vector2 Event", menuName = "Events/Vector2 Event")]
    public class Vector2EventSO : ScriptableObject
    {
        private readonly List<Vector2EventListener> _eventListenerList = new();
        public bool HasAnyDependent;
        [ShowIf("HasAnyDependent")] public List<Vector2EventSO> EventsDependOnThis;

        public void Raise(Vector2 param)
        {
            for (var i = _eventListenerList.Count - 1; i >= 0; i--)
                _eventListenerList[i].OnEventRaised(param);
        }

        [ShowIf("HasAnyDependent")]
        public void RaiseSelfAndDependents(Vector2 param)
        {
            Raise(param);

            if (HasAnyDependent && EventsDependOnThis != null)
                for (var i = 0; i < EventsDependOnThis.Count; i++)
                    EventsDependOnThis[i].Raise(param);
        }

        public void RegisterListener(Vector2EventListener listener)
        {
            _eventListenerList.Add(listener);
        }

        public void UnregisterListener(Vector2EventListener listener)
        {
            _eventListenerList.Remove(listener);
        }
    }
}