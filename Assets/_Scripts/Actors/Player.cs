using System;
using System.Threading.Tasks;
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
        [SerializeField] private Vector3IntSO _playerCoord;
        [SerializeField] private GameStateSystemSO _gameStateSystem;
        [SerializeField] private MovementSystemSO _movementSystem;

        private Vector3Int _destinationCoord;

        private bool _isMoving;
        private bool _isSelected;
        
        [SerializeField] private int _moveRange = 3;
        public int AttackRange;
        [SerializeField] private int _damage;
        [SerializeField] private int _health;
        [SerializeField] private AnimatorController _animatorController;

        private void Awake()
        {
            Vector3 position = transform.position;
            _playerPositionSO.Value = position;
            _playerCoord.Value = _tilemap.WorldToCell(position);
        }

        private void Start()
        {
            _movableAttributeChangeTilePos.Raise(_tilemap.WorldToCell(transform.position));
        }

        public void OnPlayerSelected()
        {
            _playerPositionSO.Value = transform.position;
        }

        public async void OnMove(Vector2 mousePosition)
        {
            _destinationCoord = _tilemap.WorldToCell(mousePosition);
            if (IsMovePossible(_destinationCoord))
            {
                _isMoving = true;
                await _movementSystem.MovePlayer(_destinationCoord, _moveRange);
            }
            _isMoving = false;
            _playerCoord.Value = _destinationCoord;
            AfterPlayerTurn();
        }

        private void AfterPlayerTurn()
        {
            _playerPositionSO.Value = transform.position;
            _gameStateSystem.Raise(State.EnemyTurn);
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