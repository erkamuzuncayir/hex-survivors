using UnityEngine;
using UnityEngine.Events;

namespace _Scripts.Events
{
    /// <summary>
    ///     This class listens to the IntEventSO.
    ///     When the event raised, it calls the methods assigned to it with a int parameter.
    /// </summary>
    public class IntEventListener : MonoBehaviour
    {
        public IntEventSO GameEventSO;
        public UnityEvent<int> Response;

        private void OnEnable()
        {
            GameEventSO.RegisterListener(this);
        }

        private void OnDisable()
        {
            GameEventSO.UnregisterListener(this);
        }


        public void OnEventRaised(int param)
        {
            Response.Invoke(param);
        }
    }
}