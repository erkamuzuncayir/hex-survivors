using _Scripts.Actors;
using _Scripts.Data.RuntimeSets;
using _Scripts.Data.Type;
using _Scripts.Systems;
using UnityEngine;

namespace _Scripts.Managers
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] private GameStateSystemSO _gameStateSystem;
        [SerializeField] private GameObjectRuntimeSet _enemyRuntimeSet;
        [SerializeField] private EnemyRuntimeSet _enemyScriptRuntimeSet;
        [SerializeField] private Vector3SO _playerPositionSO;
        private GameObject _instanceEnemy;
        private int _enemyIndex;
        
        private void Awake()
        {
            _enemyRuntimeSet.Initialize();
        }

        /*
    public void DebugMe()
    {
        _enemyPool.Get();
    }

    public void KillIt()
    {
        _enemyPool.Release(_instanceEnemy);
    }
    */

        public void OnEnemyTurn(State turn)
        {
            if(turn != State.EnemyTurn)
                return;

            _enemyIndex = 0;
            ProcessEnemyTurn();
            AfterEnemyTurn();
        }

        public void ProcessEnemyTurn()
        {
            if (_enemyIndex == _enemyScriptRuntimeSet.Items.Count)
            {
                AfterEnemyTurn();
                return;
            }
            _enemyScriptRuntimeSet.Items[_enemyIndex++].OnEnemyTurn();
        }
        
        private void AfterEnemyTurn()
        {
            _gameStateSystem.Raise(State.PlayerTurn);
        }
    }
}