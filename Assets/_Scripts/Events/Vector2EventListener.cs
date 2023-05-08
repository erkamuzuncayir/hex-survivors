using UnityEngine;
using UnityEngine.Events;

namespace _Scripts.Events
{
    /// <summary>
    ///     This class listens to the Vector2EventSO.
    ///     When the event raised, it calls the methods assigned to it with a Vector2 parameter.
    /// </summary>
    public class Vector2EventListener : MonoBehaviour
    {
        public Vector2EventSO GameEventSO;
        public UnityEvent<Vector2> Response;

        private void OnEnable()
        {
            GameEventSO.RegisterListener(this);
        }

        private void OnDisable()
        {
            GameEventSO.UnregisterListener(this);
        }

        public void OnEventRaised(Vector2 param)
        {
            Response.Invoke(param);
        }
    }
}