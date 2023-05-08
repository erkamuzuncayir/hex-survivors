using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace _Scripts.Events
{
    /// <summary>
    ///     This class inherits scriptable object to create customized event layer for project.
    /// </summary>
    [CreateAssetMenu(fileName = "Float Event", menuName = "Events/Float Event")]
    public class FloatEventSO : ScriptableObject
    {
        private readonly List<FloatEventListener> _eventListenerList = new();
        public bool HasAnyDependent;
        [ShowIf("HasAnyDependent")] public List<FloatEventSO> EventsDependOnThis;

        public void Raise(float param)
        {
            for (int i = _eventListenerList.Count - 1; i >= 0; i--)
                _eventListenerList[i].OnEventRaised(param);
        }

        [ShowIf("HasAnyDependent")]
        public void RaiseSelfAndDependents(float param)
        {
            Raise(param);

            if (HasAnyDependent && EventsDependOnThis != null)
                for (int i = 0; i < EventsDependOnThis.Count; i++)
                    EventsDependOnThis[i].Raise(param);
        }

        public void RegisterListener(FloatEventListener listener)
        {
            _eventListenerList.Add(listener);
        }

        public void UnregisterListener(FloatEventListener listener)
        {
            _eventListenerList.Remove(listener);
        }
    }
}