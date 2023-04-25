using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

/*
 * TODO: Şuan gidilecek noktanın tilemap koordinatını çıkartabiliyorum. Şimdi iki nokta arasındaki mesafeyi x ve y'de 
 * adımlara bölerek yani her adımda bir hexa gidecek şekilde hesaplamalıyım. 
 */
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

    [SerializeField] private float _speed = 50f;
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
        _playerGridCoordinate = _tilemap.WorldToCell(transform.position);
        _inputMouse.Mouse.MouseClick.performed += _ => MouseClick();
    }

    private void MouseClick()
    {
        Vector2 mousePosition = _inputMouse.Mouse.MousePosition.ReadValue<Vector2>();
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        
        // Check for whether clicked position is a cell or not.
        _destinationGridCoordinate = _tilemap.WorldToCell(mousePosition);
        if (_tilemap.HasTile(_destinationGridCoordinate))
        {
            _playerWorldPosition = transform.position;
            _destinationWorldPosition = _tilemap.CellToWorld(_destinationGridCoordinate);

            if(_playerGridCoordinate != _destinationGridCoordinate)
                Move();
        }
    }
    

    private void Move()
    {
        Vector3 moveDirection;
        Debug.Log("player coordi: " + _playerWorldPosition);
        Debug.Log("dest coordi: " + _destinationWorldPosition);
        
        var position = transform.position;


        if (_playerWorldPosition.x > _destinationWorldPosition.x)
        {
            if (_playerWorldPosition.y > _destinationWorldPosition.y)
                moveDirection = new Vector3(-HexHalfWidth, -HexHalfHeight, 0);
            else if (_playerWorldPosition.y < _destinationWorldPosition.y)
                moveDirection = new Vector3(-HexHalfWidth, HexHalfHeight, 0);
            else
                moveDirection = new Vector3(-(HexHalfWidth * 2), 0, 0);

            StartCoroutine(ContinuousMove(transform.position, _playerWorldPosition + moveDirection, _speed));

        }
        else if (_playerWorldPosition.x < _destinationWorldPosition.x)
        {
            if (_playerWorldPosition.y > _destinationWorldPosition.y)
                moveDirection = new Vector3(HexHalfWidth, -HexHalfHeight, 0);
            else if (_playerWorldPosition.y < _destinationWorldPosition.y)
                moveDirection = new Vector3(HexHalfWidth, HexHalfHeight, 0);
            else
                moveDirection = new Vector3(HexHalfWidth * 2, 0, 0);

            StartCoroutine(ContinuousMove(transform.position, _playerWorldPosition + moveDirection, _speed));

        }
        else if(Math.Abs(_playerWorldPosition.x - _destinationWorldPosition.x) < .05f)
        {
            if (_playerWorldPosition.y > _destinationWorldPosition.y)
                moveDirection = new Vector3(HexHalfWidth, -HexHalfHeight, 0);
            else
                moveDirection = new Vector3(HexHalfWidth, HexHalfHeight, 0);
            
            StartCoroutine(ContinuousMove(transform.position, _playerWorldPosition + moveDirection, _speed));

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
