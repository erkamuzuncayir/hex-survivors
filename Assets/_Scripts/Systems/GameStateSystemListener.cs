using UnityEngine;
using UnityEngine.Events;

namespace _Scripts.Systems
{
    /// <summary>
    ///     This class listens to the GameStateEventSO.
    ///     When the event raised, it calls the methods assigned to it with a int parameter.
    /// </summary>
    public class GameStateSystemListener : MonoBehaviour
    {
        public GameStateSystemSO GameStateSystem;
        public UnityEvent<State> Response;

        private void OnEnable()
        {
            GameStateSystem.RegisterListener(this);
        }

        private void OnDisable()
        {
            GameStateSystem.UnregisterListener(this);
        }


        public void OnEventRaised(State param)
        {
            Response.Invoke(param);
        }
    }
}