using _Scripts.Data.RuntimeSets;
using _Scripts.Data.Type;
using _Scripts.Events;
using _Scripts.Systems;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace _Scripts.Actors
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private VoidEventSO _playerTurnCompleteAnnouncer;
        [SerializeField] private Vector3EventSO _movableAttributeChangeTilePos;
        [SerializeField] private GameObjectRuntimeSet _player;
        [SerializeField] private Tilemap _tilemap;
        [SerializeField] private Vector3SO _playerPositionSO;
        [SerializeField] private GameStateSystemSO _gameStateSystem;
        [SerializeField] private MovementSystemSO _movementSystem;

        private Vector3Int _destinationCoord;
        private Vector3Int _playerCoordinate;

        private bool _isMoving;
        private bool _isSelected;

        // TODO: Bu attributeleri scriptable objecte çevir. Ama enemyler için gerek yok.
        [SerializeField] private int _moveRange;
        [SerializeField] private int _attackRange;
        [SerializeField] private int _damage;
        [SerializeField] private int _health;
        [SerializeField] private AnimatorController _animatorController;

        private void Awake()
        {
            Vector3 position = transform.position;
            _playerPositionSO.Value = position;
            _movableAttributeChangeTilePos.Raise(position);
        }

        public void OnPlayerSelected()
        {
            _playerPositionSO.Value = transform.position;
        }

        private void AfterPlayerTurn()
        {
            _playerPositionSO.Value = transform.position;

            _gameStateSystem.Raise(State.EnemyTurn);
        }

        public void OnMove(Vector2 mousePosition)
        {
            _playerCoordinate = _tilemap.WorldToCell(transform.position);
            _destinationCoord = _tilemap.WorldToCell(mousePosition);
            if (IsMovePossible(_destinationCoord))
            {
                _isMoving = true;
                StartCoroutine(_movementSystem.MovePlayer(_destinationCoord,
                    isOperationCompleted =>
                    {
                        if (isOperationCompleted)
                        {
                            _isMoving = false;
                            AfterPlayerTurn();
                        }
                    }));
                _playerPositionSO.Value = _tilemap.CellToWorld(_destinationCoord);
            }
        }

        private bool IsMovePossible(Vector3Int destinationCoord)
        {
            if (_isMoving)
                return false;
            if (!_tilemap.HasTile(destinationCoord))
                return false;

            return true;
        }
    }
}