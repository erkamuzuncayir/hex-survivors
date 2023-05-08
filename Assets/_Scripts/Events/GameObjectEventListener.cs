using UnityEngine;
using UnityEngine.Events;

namespace _Scripts.Events
{
    /// <summary>
    ///     This class listens to the GameObjectEventSO.
    ///     When the event raised, it calls the methods assigned to it with a int parameter.
    /// </summary>
    public class GameObjectEventListener : MonoBehaviour
    {
        public GameObjectEventSO GameEventSO;
        public UnityEvent<GameObject> Response;

        private void OnEnable()
        {
            GameEventSO.RegisterListener(this);
        }

        private void OnDisable()
        {
            GameEventSO.UnregisterListener(this);
        }

        public void OnEventRaised(GameObject param)
        {
            Response.Invoke(param);
        }
    }
}