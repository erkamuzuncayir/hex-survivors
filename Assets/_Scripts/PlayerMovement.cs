using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour

{
    private const float HexHalfHeight = 0.75f;
    private const float HexHalfWidth = 0.5f;

    [SerializeField] private Tilemap _tilemap;
    
    private InputMouse _inputMouse;
    
    private Vector3Int _destinationGridCoordinate;
    private Vector3 _destinationWorldPosition;
    private Vector3 _playerWorldPosition;
    private Vector3Int _playerGridCoordinate;

    [SerializeField] private float _speed = 1f;
    [SerializeField] private bool _isMoving;
    
    private void Awake()
    {
        _inputMouse = new InputMouse();
    }
    
    private void OnEnable()
    {
        _inputMouse.Enable();
    }

    private void OnDisable()
    {
        _inputMouse.Disable();
    }

    private void Start()
    {
        _inputMouse.Mouse.MouseClick.performed += _ => OnMouseClick();
    }

    private void OnMouseClick()
    {
     
        Vector2 mousePosition = _inputMouse.Mouse.MousePosition.ReadValue<Vector2>();
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        
        // Check for whether clicked position is a cell or not.
        _playerGridCoordinate = _tilemap.WorldToCell(transform.position);
        if (!_isMoving && _tilemap.HasTile(_tilemap.WorldToCell(mousePosition)))
        {        
            _destinationGridCoordinate = _tilemap.WorldToCell(mousePosition);
         
            if (_playerGridCoordinate != _destinationGridCoordinate)
            {
                StartCoroutine(Move());
                _isMoving = true;
            }
        }
    }
    

    private IEnumerator Move()
    {   
        _playerWorldPosition = transform.position;
        _destinationWorldPosition = _tilemap.CellToWorld(_destinationGridCoordinate);
        Vector3 moveDirection;
        

        if (_playerWorldPosition.x > _destinationWorldPosition.x)
        {
            if (_playerWorldPosition.y > _destinationWorldPosition.y)
                moveDirection = new Vector3(-HexHalfWidth, -HexHalfHeight, 0);
            else if (_playerWorldPosition.y < _destinationWorldPosition.y)
                moveDirection = new Vector3(-HexHalfWidth, HexHalfHeight, 0);
            else
                moveDirection = new Vector3(-(HexHalfWidth * 2), 0, 0);
            
            yield return ContinuousMove(transform.position, _playerWorldPosition + moveDirection, _speed);

        }
        else if (_playerWorldPosition.x < _destinationWorldPosition.x)
        {
            if (_playerWorldPosition.y > _destinationWorldPosition.y)
                moveDirection = new Vector3(HexHalfWidth, -HexHalfHeight, 0);
            else if (_playerWorldPosition.y < _destinationWorldPosition.y)
                moveDirection = new Vector3(HexHalfWidth, HexHalfHeight, 0);
            else
                moveDirection = new Vector3(HexHalfWidth * 2, 0, 0);

            yield return ContinuousMove(transform.position, _playerWorldPosition + moveDirection, _speed);

        }
        else if(Math.Abs(_playerWorldPosition.x - _destinationWorldPosition.x) < .05f)
        {
            if (_playerWorldPosition.y > _destinationWorldPosition.y)
                moveDirection = new Vector3(HexHalfWidth, -HexHalfHeight, 0);
            else
                moveDirection = new Vector3(HexHalfWidth, HexHalfHeight, 0);
            
            yield return ContinuousMove(transform.position, _playerWorldPosition + moveDirection, _speed);

        }
        
        _playerWorldPosition = transform.position;

        if (Vector3.Distance(_playerWorldPosition, _destinationWorldPosition) < .05f)
        {
            _isMoving = false;
        }
        else
        {
            yield return null;
            StartCoroutine(Move());
        }
    }

    private IEnumerator ContinuousMove(Vector3 start, Vector3 end, float speed)
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
}
