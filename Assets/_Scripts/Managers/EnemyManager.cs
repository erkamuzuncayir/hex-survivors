using System.Threading.Tasks;
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
        [SerializeField] private GameObjectRuntimeSet _playerRuntimeSet;
        [SerializeField] private GameObjectRuntimeSet _enemyRuntimeSet;
        [SerializeField] private EnemyRuntimeSet _enemyScriptRuntimeSet;
        [SerializeField] private Vector3IntSO _playerCoord;
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

        public async void OnEnemyTurn(State turn)
        {
            if(turn != State.EnemyTurn)
                return;

            for (int i = 0; i < _enemyScriptRuntimeSet.Items.Count; i++)
            {
                await _enemyScriptRuntimeSet.Items[i].OnEnemyTurn(_playerCoord.Value);
            }

            AfterEnemyTurn();
        }

        private void AfterEnemyTurn()
        {
            _gameStateSystem.Raise(State.PlayerTurn);
        }
    }
}