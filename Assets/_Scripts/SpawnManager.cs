using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using NaughtyAttributes.Test;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    private ObjectPool<GameObject> _enemyPool;
    [SerializeField] private Tilemap _tilemap;
    [SerializeField] private GameObject[] _enemyPrefabs;
    [SerializeField] private GameObjectRuntimeSet _enemyRuntimeSet;
    [SerializeField] private Vector3SO _playerPositionSO;

    private void Awake()
    {
        _enemyPool = new ObjectPool<GameObject>(() =>
            {
                return Instantiate(GetRandomEnemy());
            },
            enemy =>
            {
                // Check enemy count
                //_enemyRuntimeSet.AddToList(enemy);
                enemy.transform.position = GetRandomPosition();
                enemy.gameObject.SetActive(true);
            },
            enemy =>
            {
                // Check enemy count
                //_enemyRuntimeSet.RemoveFromList(enemy);
                enemy.gameObject.SetActive(false);
            },
            enemy =>
            {
                Destroy(enemy.gameObject);
            },
            true,
            10,
            20
        );
    }

    [Button()]
    public void SpawnEnemy()
    {
        _enemyPool.Get();
    }

    public void RemoveEnemy(GameObject deadEnemy)
    {
        _enemyPool.Release(deadEnemy);
    }
    private GameObject GetRandomEnemy()
    {
        return _enemyPrefabs[Random.Range(0, _enemyPrefabs.Length)];
    }
    
    private Vector3 GetRandomPosition()
    {
        Vector3 randPos;

        do
        {
            randPos = new Vector3(Random.Range(_playerPositionSO.value.x - 3, _playerPositionSO.value.x + 3),
                Random.Range(_playerPositionSO.value.y - 3, _playerPositionSO.value.y + 3), 0);
        } while ((Vector3.Distance(randPos, _playerPositionSO.value) < 3) &&
                 _tilemap.HasTile(_tilemap.WorldToCell(randPos)));

        return randPos;
    }
}
