using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObjectRuntimeSet _player;
    [SerializeField] private Tilemap _tilemap;
    [SerializeField] private Vector3SO _playerPositionSO;
    [SerializeField] private StateSO _gameState;
    [SerializeField] private MovementSystemSO _movementSystem;

    private Vector3Int _destinationCoord;
    private Vector3Int _playerCoordinate;

    private bool _isMoving;
    private bool _isSelected;
    private int _actionCount;
    
    private void Awake()
    {
        _playerPositionSO.value = transform.position;
    }

    [Button()]
    public void DebugMe()
    {
        Debug.Log(_tilemap.WorldToCell(transform.position));
    }
    /*
    public void StateCheck()
    {
        if (_gameState.GameState == StateSO.State.PlayerTurn)
        {
            OnPlayerTurn();
            
            AfterPlayerTurn();
        }
        
    }
    */
    
    public void OnPlayerSelected()
    {
        _playerPositionSO.value = transform.position;

        
    }

    private void AfterPlayerTurn()
    {
        _playerPositionSO.value = transform.position;

        _gameState.GameState = StateSO.State.EnemyTurn;
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
                        _isMoving = false;
                }));
            _playerPositionSO.value = _tilemap.CellToWorld(_destinationCoord);
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