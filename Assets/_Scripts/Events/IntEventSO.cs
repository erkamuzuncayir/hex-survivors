using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace _Scripts.Events
{
    /// <summary>
    ///     This class inherits scriptable object to create customized event layer for project.
    /// </summary>
    [CreateAssetMenu(fileName = "Int Event", menuName = "Events/Int Event")]
    public class IntEventSO : ScriptableObject
    {
        private readonly List<IntEventListener> _eventListenerList = new();
        public bool HasAnyDependent;
        [ShowIf("HasAnyDependent")] public List<IntEventSO> EventsDependOnThis;

        public void Raise(int param)
        {
            for (int i = _eventListenerList.Count - 1; i >= 0; i--)
                _eventListenerList[i].OnEventRaised(param);
        }

        [ShowIf("HasAnyDependent")]
        public void RaiseSelfAndDependents(int param)
        {
            Raise(param);

            if (HasAnyDependent && EventsDependOnThis != null)
                for (int i = 0; i < EventsDependOnThis.Count; i++)
                    EventsDependOnThis[i].Raise(param);
        }

        public void RegisterListener(IntEventListener listener)
        {
            _eventListenerList.Add(listener);
        }

        public void UnregisterListener(IntEventListener listener)
        {
            _eventListenerList.Remove(listener);
        }
    }
}