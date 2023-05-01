using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    private Action<bool> _isOperationCompleted;
    [SerializeField] private Vector3SO _playerPositionSO;
    [SerializeField] private StateSO _gameState;
    [SerializeField] private MovementSystemSO _movementSystem;
    [SerializeField] private Tilemap _tilemap;

    private Vector3Int _destinationCoordinate;
    private Vector3Int _playerCoordinate;

    private bool _isMoving;
    
    private void Awake()
    {
        _playerPositionSO.value = transform.position;
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
                StartCoroutine(_movementSystem.Move(_tilemap, gameObject, _destinationCoordinate,
                    _isOperationCompleted =>
                    {
                        if (_isOperationCompleted)
                            _isMoving = false;
                    }));
                _playerPositionSO.value = _tilemap.CellToWorld(_destinationCoordinate);
                _isMoving = true;
            }
        }
    }
}