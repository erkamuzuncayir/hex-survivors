using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    private const float TileHalfHeight = 0.75f;
    private const float TileHalfWidth = 0.5f;
    [SerializeField] private Vector3SO _playerPositionSO;
    [FormerlySerializedAs("_pathfinding")] [SerializeField] private PathfindingSystem _pathfindingSystem;

    [SerializeField] private Tilemap _tilemap;

    private Vector3Int _destinationCoordinate;
    private Vector3 _destinationPosition;
    private Vector3 _playerPosition;
    private Vector3Int _playerCoordinate;

    [SerializeField] private float _speed = 1f;
    [SerializeField] private bool _isMoving;

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
                StartCoroutine(Move());
                _isMoving = true;
            }
        }
    }
    
    private IEnumerator Move()
    {   
        _playerPosition = transform.position;
        _destinationPosition = _tilemap.CellToWorld(_destinationCoordinate);

        List<Vector3Int> moves =
            _pathfindingSystem.FindNextMove(_tilemap, _tilemap.WorldToCell(_playerPosition), _destinationCoordinate);

        foreach (var m in moves)
        {
            yield return ContinuousMove(_tilemap.CellToWorld(m), _speed);
        }
        /*
        Vector3 moveDirection;

        
        if (_playerPosition.x > _destinationPosition.x)
        {
            if (_playerPosition.y > _destinationPosition.y)
                moveDirection = new Vector3(-TileHalfWidth, -TileHalfHeight, 0);
            else if (_playerPosition.y < _destinationPosition.y)
                moveDirection = new Vector3(-TileHalfWidth, TileHalfHeight, 0);
            else
                moveDirection = new Vector3(-(TileHalfWidth * 2), 0, 0);
            
            yield return ContinuousMove(_playerPosition + moveDirection, _speed);

        }
        else if (_playerPosition.x < _destinationPosition.x)
        {
            if (_playerPosition.y > _destinationPosition.y)
                moveDirection = new Vector3(TileHalfWidth, -TileHalfHeight, 0);
            else if (_playerPosition.y < _destinationPosition.y)
                moveDirection = new Vector3(TileHalfWidth, TileHalfHeight, 0);
            else
                moveDirection = new Vector3(TileHalfWidth * 2, 0, 0);

            yield return ContinuousMove(_playerPosition + moveDirection, _speed);

        }
        else if(Math.Abs(_playerPosition.x - _destinationPosition.x) < .05f)
        {
            if (_playerPosition.y > _destinationPosition.y)
                moveDirection = new Vector3(TileHalfWidth, -TileHalfHeight, 0);
            else
                moveDirection = new Vector3(TileHalfWidth, TileHalfHeight, 0);
            
            yield return ContinuousMove(_playerPosition + moveDirection, _speed);

        }

        */
        if (Vector3.Distance(_playerPosition, _destinationPosition) < .05f)
        {
            _playerPosition = transform.position;
            WritePlayerPositionToSO();
            _isMoving = false;
        }
        else
        {
            yield return null;
            StartCoroutine(Move());
        }
    }

    private IEnumerator ContinuousMove(Vector3 end, float speed)
    {
        float t = 0;
        while (t < 1)
        {
            transform.position = Vector3.MoveTowards(transform.position, end, t);
            t = t + Time.deltaTime / speed;
            yield return new WaitForEndOfFrame();
        }

        transform.position = end;
    }

    public void WritePlayerPositionToSO()
    {
        _playerPositionSO.value = transform.position;
    }
}
