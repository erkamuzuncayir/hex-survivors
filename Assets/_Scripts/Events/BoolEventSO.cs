using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace _Scripts.Events
{
    /// <summary>
    ///     This class inherits scriptable object to create customized event layer for project.
    /// </summary>
    [CreateAssetMenu(fileName = "Bool Event", menuName = "Events/Bool Event")]
    public class BoolEventSO : ScriptableObject
    {
        private readonly List<BoolEventListener> _eventListenerList = new();
        public bool HasAnyDependent;
        [ShowIf("HasAnyDependent")] public List<VoidEventSO> EventsDependOnThis;

        public void Raise(bool param)
        {
            for (var i = _eventListenerList.Count - 1; i >= 0; i--)
                _eventListenerList[i].OnEventRaised(param);
        }

        [ShowIf("HasAnyDependent")]
        public void RaiseSelfAndDependents(bool param)
        {
            Raise(param);

            if (HasAnyDependent && EventsDependOnThis != null)
                for (var i = 0; i < EventsDependOnThis.Count; i++)
                    EventsDependOnThis[i].Raise();
        }

        public void RegisterListener(BoolEventListener listener)
        {
            _eventListenerList.Add(listener);
        }

        public void UnregisterListener(BoolEventListener listener)
        {
            _eventListenerList.Remove(listener);
        }
    }
}