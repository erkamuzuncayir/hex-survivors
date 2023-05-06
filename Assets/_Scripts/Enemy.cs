using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObjectEvent _deathAnnouncer;
    [SerializeField] private MovementSystemSO _movementSystem;
    [SerializeField] private GameObjectRuntimeSet _enemies;
    [SerializeField] private EnemyRuntimeSet _enemyScripts;
    [SerializeField] private Tilemap _tilemap;
    
    private Vector3Int _destinationCoordinate;
    private Vector3Int _enemyCoordinate;
    
    private bool _isMoving;
    [SerializeField] private int _moveRange;
    [SerializeField] private int _attackRange;
    [SerializeField] private int _damage;
    [SerializeField] private int _health;
    [SerializeField] private AnimatorController _animatorController;
    
    //ENEMYİ YARAT ARTIK. AKSİYONLARINI VE METHODLARINI EVENTLERİ FALAN
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
    
    public void OnEnemyTurn(Vector3SO playerPositionSO)
    {
        MoveAction(playerPositionSO);
    }

    public void OnDeath()
    {
        _deathAnnouncer.Raise(gameObject);
    }
    public void MoveAction(Vector3SO playerPositionSO)
    {
        // Check for whether clicked position is a cell or not.
        _enemyCoordinate = _tilemap.WorldToCell(transform.position);
        if (!_isMoving)
        {        
            _destinationCoordinate = _tilemap.WorldToCell(playerPositionSO.value);
//            if (_enemyCoordinate != _destinationCoordinate)
//            {
//                StartCoroutine(_movementSystem.MoveEnemy(gameObject,
//                    isOperationCompleted =>
//                    {
//                        if (isOperationCompleted)
//                            _isMoving = false;
//                    }));
//                playerPositionSO.value = _tilemap.CellToWorld(_destinationCoordinate);
//                _isMoving = true;
//            }
        }
    }
}