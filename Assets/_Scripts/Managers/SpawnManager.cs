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
    [SerializeField] private Vector3Event _movableAttributeChangeTilePos;
    [SerializeField] private Tilemap _tilemap;
    [SerializeField] private TileDictionarySO _tileDictionary;
    [SerializeField] private GameObject[] _enemyPrefabs;
    [SerializeField] private GameObjectRuntimeSet _enemyRuntimeSet;
    [SerializeField] private Vector3SO _playerPositionSO;
    private ObjectPool<GameObject> _enemyPool;

    private void Awake()
    {
        _enemyPool = new ObjectPool<GameObject>(() =>
            {
                return Instantiate(GetRandomEnemy());
            },
            enemy =>
            {
                OnEnemySpawn(enemy);
                // Check enemy count
                //_enemyRuntimeSet.AddToList(enemy);
            },
            enemy =>
            {
                // Check enemy count
                //_enemyRuntimeSet.RemoveFromList(enemy);
                OnEnemyDeath(enemy);
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

    public void OnEnemySpawn(GameObject enemy)
    {
        enemy.transform.position = GetRandomPosition();
        enemy.gameObject.SetActive(true);
        _movableAttributeChangeTilePos.Raise(_tilemap.WorldToCell(enemy.transform.position));
    }

    public void OnEnemyDeath(GameObject enemy)
    {
        enemy.gameObject.SetActive(false);
        _movableAttributeChangeTilePos.Raise(_tilemap.WorldToCell(enemy.transform.position));
        _enemyPool.Release(enemy);
    }
    private GameObject GetRandomEnemy()
    {
        return _enemyPrefabs[Random.Range(0, _enemyPrefabs.Length)];
    }
    
    private Vector3 GetRandomPosition()
    {
        Vector3Int randPos;
        Vector3Int playerCoord = _tilemap.WorldToCell(_playerPositionSO.value);
        
        do
        {
            randPos = new Vector3Int(Random.Range(playerCoord.x - 4, playerCoord.x + 5),
                Random.Range(playerCoord.y - 4, playerCoord.y + 5), 0);
        } while (Vector3.Distance(randPos, playerCoord) < 3 &&
                 _tilemap.HasTile(randPos));

        return _tilemap.CellToWorld(randPos);
    }
}
