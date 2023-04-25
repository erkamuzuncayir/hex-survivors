using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

// TODO: Şuan gidilecek noktanın tilemap koordinatını çıkartabiliyorum. Şimdi iki nokta arasındaki mesafeyi x ve y'de 
// adımlara bölerek yani her adımda bir hexa gidecek şekilde hesaplamalıyım. 
public class PlayerMovement : MonoBehaviour
{
    public Tilemap tilemap;
    private InputMouse _inputMouse;
    private Vector3Int _destinationGridCoordinate;
    private Vector3Int _initialGridCoordinate;

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
        _initialGridCoordinate = tilemap.WorldToCell(transform.position);
        _inputMouse.Mouse.MouseClick.performed += _ => MouseClick();
    }

    private void MouseClick()
    {
        Vector2 mousePosition = _inputMouse.Mouse.MousePosition.ReadValue<Vector2>();
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        
        // Check for whether clicked position is a cell or not.
        _destinationGridCoordinate = tilemap.WorldToCell(mousePosition);
        if (!tilemap.HasTile(_destinationGridCoordinate))
            return;
        
        var playerCoordinate = tilemap.WorldToCell(transform.position);
        var destinationCoordinate = tilemap.WorldToCell(mousePosition);
        Debug.Log("player coordinate:" + playerCoordinate);
        Debug.Log("destination coordinate:" + destinationCoordinate);
        Debug.Log("I am:" + tilemap.GetTile<VariableTile>(destinationCoordinate).isEmpty);
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        
        if (Vector3.Distance(transform.position, _destinationGridCoordinate) > 0.1f)
            transform.position = Vector3.MoveTowards(transform.position, _destinationGridCoordinate, 5 * Time.deltaTime);
    }
        
        
}
