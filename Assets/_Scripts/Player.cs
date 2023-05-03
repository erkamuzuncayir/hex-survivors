using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObjectRuntimeSet _playerRuntimeReference;
    [SerializeField] private Tilemap _tilemap;
    [SerializeField] private Vector3SO _playerPositionSO;
    [SerializeField] private StateSO _gameState;
    [SerializeField] private MovementSystemSO _movementSystem;

    private Vector3Int _destinationCoordinate;
    private Vector3Int _playerCoordinate;

    private bool _isMoving;
    private int _actionCount;
    
    private void Awake()
    {
        _playerPositionSO.value = transform.position;
    }

    public void StateCheck()
    {
        if(_gameState.GameState == StateSO.State.PlayerTurn)
            OnPlayerTurn();
        
        AfterPlayerTurn();
    }
    public void OnPlayerTurn()
    {
        
    }

    private void AfterPlayerTurn()
    {
        _gameState.GameState = StateSO.State.EnemyTurn;
    }
    
    public void OnMouseClick(Vector2 mousePosition)
    {
        // Check for whether clicked position is a cell or not.
        _playerCoordinate = _tilemap.WorldToCell(transform.position);
        if (!_isMoving && _tilemap.HasTile(_tilemap.WorldToCell(mousePosition)))
        {        
            _destinationCoordinate = _tilemap.WorldToCell(mousePosition);
         
            if (_playerCoordinate != _destinationCoordinate)
            {
                StartCoroutine(_movementSystem.Move(gameObject, _destinationCoordinate,
                    isOperationCompleted =>
                    {
                        if (isOperationCompleted)
                            _isMoving = false;
                    }));
                _playerPositionSO.value = _tilemap.CellToWorld(_destinationCoordinate);
                _isMoving = true;
            }
        }
    }
}