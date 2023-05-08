using UnityEngine;
using UnityEngine.Events;

namespace _Scripts.Events
{
    /// <summary>
    ///     This class listens to the VoidEventSO.
    ///     When the event raised, it calls the methods assigned to it.
    /// </summary>
    public class VoidEventListener : MonoBehaviour
    {
        public VoidEventSO GameEventSO;
        public UnityEvent Response;

        private void OnEnable()
        {
            GameEventSO.RegisterListener(this);
        }

        private void OnDisable()
        {
            GameEventSO.UnregisterListener(this);
        }

        public void OnEventRaised()
        {
            Response.Invoke();
        }
    }
}