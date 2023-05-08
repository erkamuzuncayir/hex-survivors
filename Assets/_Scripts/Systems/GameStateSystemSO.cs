using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace _Scripts.Systems
{
    /// <summary>
    ///     This class inherits scriptable object to create an event based state machine for project.
    /// </summary>
    
    public enum State
    {
        MainMenu,
        PlayerTurn,
        EnemyTurn
    }
    
    [CreateAssetMenu(fileName = "GameStateSystem", menuName = "Game State System")]
    public class GameStateSystemSO : ScriptableObject
    {
        public State GameState = State.PlayerTurn;
        private List<GameStateSystemListener> _eventListenerList = new();
        public bool HasAnyDependent;
        [ShowIf("HasAnyDependent")] public List<GameStateSystemSO> EventsDependOnThis;

        public void Raise(State param)
        {
            GameState = param;
            for (var i = _eventListenerList.Count - 1; i >= 0; i--)
                _eventListenerList[i].OnEventRaised(param);
        }

        [ShowIf("HasAnyDependent")]
        public void RaiseSelfAndDependents(State param)
        {
            GameState = param;
            Raise(param);

            if (HasAnyDependent && EventsDependOnThis != null)
                for (var i = 0; i < EventsDependOnThis.Count; i++)
                    EventsDependOnThis[i].Raise(param);
        }

        public void RegisterListener(GameStateSystemListener listener)
        {
            _eventListenerList.Add(listener);
        }

        public void UnregisterListener(GameStateSystemListener listener)
        {
            _eventListenerList.Remove(listener);
        }
    }


}