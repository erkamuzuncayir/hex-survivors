using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace _Scripts.Events
{
    /// <summary>
    ///     This class inherits scriptable object to create customized event layer for project.
    /// </summary>
    [CreateAssetMenu(fileName = "Void Event", menuName = "Events/Void Event")]
    public class VoidEventSO : ScriptableObject
    {
        private readonly List<VoidEventListener> _eventListenerList = new();
        public bool HasAnyDependent;
        [ShowIf("HasAnyDependent")] public List<VoidEventSO> EventsDependOnThis;

        [Button]
        public void Raise()
        {
            for (var i = _eventListenerList.Count - 1; i >= 0; i--)
                _eventListenerList[i].OnEventRaised();
        }

        [ShowIf("HasAnyDependent"), Button("Raise Self and Dependents")]
        public void RaiseSelfAndDependents()
        {
            Raise();

            if (HasAnyDependent && EventsDependOnThis != null)
                for (var i = 0; i < EventsDependOnThis.Count; i++)
                    EventsDependOnThis[i].Raise();
        }

        public void RegisterListener(VoidEventListener listener)
        {
            _eventListenerList.Add(listener);
        }

        public void UnregisterListener(VoidEventListener listener)
        {
            _eventListenerList.Remove(listener);
        }
    }
}