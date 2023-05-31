using _Scripts.Actors;
using _Scripts.Data.Collections;
using _Scripts.Data.RuntimeSets;
using _Scripts.Data.Type;
using _Scripts.Events;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Tilemaps;

namespace _Scripts.Managers
{
    public class SpawnManager : MonoBehaviour
    {
        [SerializeField] private Vector3EventSO _movableAttributeChangeTilePos;
        [SerializeField] private Tilemap _tilemap;
        [SerializeField] private TileDictionarySO _tileDictionary;
        [SerializeField] private GameObject[] _enemyPrefabs;
        [SerializeField] private GameObjectRuntimeSet _enemyRuntimeSet;
        [SerializeField] private Vector3SO _playerPositionSO;
        private ObjectPool<GameObject> _enemyPool;
        [SerializeField] private int _testSpawnCount;
        private void Awake()
        {
            _enemyPool = new ObjectPool<GameObject>(() => Instantiate(GetRandomEnemy()),
                OnEnemySpawn,
                OnEnemyDeath,
                enemy => { Destroy(enemy.gameObject); },
                true,
                10,
                20
            );
        }

        public void Start()
        {
            for (int i = 0; i < _testSpawnCount; i++)
            {
                _enemyPool.Get();
            }
        }

        private void OnEnemySpawn(GameObject enemy)
        {
            Vector3 enemyPosition = GetRandomPosition();
            enemy.transform.position = enemyPosition;
            Vector3Int enemyCoord = _tilemap.WorldToCell(enemyPosition);
            enemy.gameObject.GetComponent<Enemy>().Coord = enemyCoord;
            enemy.gameObject.SetActive(true);
            _movableAttributeChangeTilePos.Raise(enemyCoord);
        }

        private void OnEnemyDeath(GameObject enemy)
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
            Vector3Int playerCoord = _tilemap.WorldToCell(_playerPositionSO.Value);

            do
            {
                randPos = new Vector3Int(Random.Range(playerCoord.x - 4, playerCoord.x + 5),
                    Random.Range(playerCoord.y - 4, playerCoord.y + 5), 0);
            } while (Vector3.Distance(randPos, playerCoord) < 3 &&
                     _tilemap.HasTile(randPos));

            return _tilemap.CellToWorld(randPos);
        }
    }
}