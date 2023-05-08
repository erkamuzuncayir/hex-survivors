using UnityEngine;
using UnityEngine.Events;

namespace _Scripts.Events
{
    /// <summary>
    ///     This class listens to the BoolEventSO.
    ///     When the event raised, it can call the methods assigned to it with a boolean parameter.
    /// </summary>
    public class BoolEventListener : MonoBehaviour
    {
        public BoolEventSO GameEventSO;
        public UnityEvent<bool> Response;

        private void OnEnable()
        {
            GameEventSO.RegisterListener(this);
        }

        private void OnDisable()
        {
            GameEventSO.UnregisterListener(this);
        }

        public void OnEventRaised(bool param)
        {
            Response.Invoke(param);
        }
    }
}