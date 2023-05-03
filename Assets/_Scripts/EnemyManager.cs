using System;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private StateSO _gameState;
    [SerializeField] private GameObjectRuntimeSet _enemyRuntimeSet;
    [SerializeField] private EnemyRuntimeSet _enemyScriptRuntimeSet;
    [SerializeField] private Vector3SO _playerPositionSO;
    private GameObject _instanceEnemy;
    
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
    
    public void StateCheck()
    {
        if(_gameState.GameState == StateSO.State.EnemyTurn)
            OnEnemyTurn();
        
        // Spawn Enemy
    }

    public void OnEnemyTurn()
    {
        for (int i = 0; i < _enemyScriptRuntimeSet.items.Count; i++)
        {
            _enemyScriptRuntimeSet.items[i].OnEnemyTurn(_playerPositionSO);
        }

        AfterEnemyTurn();
    }

    private void AfterEnemyTurn()
    {
        _gameState.GameState = StateSO.State.PlayerTurn;
    }

    private Vector3 GetRandomPosition()
    {
        Vector3 randPos;

        do
        {
            randPos = new Vector3(Random.Range(_playerPositionSO.value.x - 3, _playerPositionSO.value.x + 3),
                Random.Range(_playerPositionSO.value.y - 3, _playerPositionSO.value.y + 3), 0);
        } while (Vector3.Distance(randPos, _playerPositionSO.value) < 3);

        return randPos;
    }
}
