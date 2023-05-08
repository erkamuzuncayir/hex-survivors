using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace _Scripts.Events
{
    /// <summary>
    ///     This class inherits scriptable object to create customized event layer for project.
    /// </summary>
    [CreateAssetMenu(fileName = "Vector3 Event", menuName = "Events/Vector3 Event")]
    public class Vector3EventSO : ScriptableObject
    {
        private List<Vector3EventListener> _eventListenerList = new();
        public bool HasAnyDependent;
        [ShowIf("HasAnyDependent")] public List<Vector3EventSO> EventsDependOnThis;

        [Button]
        public void Raise(Vector3 param)
        {
            for (var i = _eventListenerList.Count - 1; i >= 0; i--)
                _eventListenerList[i].OnEventRaised(param);
        }

        [ShowIf("HasAnyDependent")]
        public void RaiseSelfAndDependents(Vector3 param)
        {
            Raise(param);

            if (HasAnyDependent && EventsDependOnThis != null)
                for (var i = 0; i < EventsDependOnThis.Count; i++)
                    EventsDependOnThis[i].Raise(param);
        }

        public void RegisterListener(Vector3EventListener listener)
        {
            _eventListenerList.Add(listener);
        }

        public void UnregisterListener(Vector3EventListener listener)
        {
            _eventListenerList.Remove(listener);
        }
    }
}