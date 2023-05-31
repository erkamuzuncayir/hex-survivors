using System.Threading.Tasks;
using _Scripts.Data.Collections;
using _Scripts.Data.RuntimeSets;
using _Scripts.Data.Type;
using _Scripts.Events;
using _Scripts.Systems;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace _Scripts.Actors
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private TileDictionarySO _tileDictionary;
        [SerializeField] private GameObjectEventSO _deathAnnouncer;
        [SerializeField] private VoidEventSO _turnCompleteAnnouncer;
        [SerializeField] private MovementSystemSO _movementSystem;
        [SerializeField] private GameObjectRuntimeSet _enemies;
        [SerializeField] private EnemyRuntimeSet _enemyScripts;
        private Vector3Int _destinationCoordinate;

        private bool _isMoving;

        public Vector3Int Coord { get; set; }
        [SerializeField] private int _moveRange = 2;
        public int AttackRange;
        [SerializeField] private int _damage;
        [SerializeField] private int _health;
        [SerializeField] private AnimatorController _animatorController;
        
        private void OnEnable()
        {
            _enemyScripts.AddToList(this);
            _enemies.AddToList(gameObject);
        }

        private void OnDisable()
        {
            _enemyScripts.RemoveFromList(this);
            _enemies.RemoveFromList(gameObject);
        }

        public async Task OnEnemyTurn(Vector3Int playerCoord)
        {
            
            // TODO: If player isn't attack range.
            if (_tileDictionary.GetTileFromDictionary(Coord).GetDistance(
                    _tileDictionary.GetTileFromDictionary(playerCoord)) > AttackRange)
            {
                await MoveAction();
            }
        }

        public void OnDeath()
        {
            _deathAnnouncer.Raise(gameObject);
        }

        public async Task MoveAction()
        {
            // Check for whether clicked position is a cell or not.
            if (!_isMoving)
            {
                _isMoving = true;
                
                await _movementSystem.MoveEnemy(gameObject, _moveRange, newCoord =>
                {
                    Coord = newCoord;
                });
                
                _isMoving = false;
            }
        }
        
    }
}