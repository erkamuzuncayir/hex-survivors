using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// TODO: Şuan gidilecek noktanın tilemap koordinatını çıkartabiliyorum. Şimdi iki nokta arasındaki mesafeyi x ve y'de 
// adımlara bölerek yani her adımda bir hexa gidecek şekilde hesaplamalıyım. 
public class PlayerMovement : MonoBehaviour
{
    public Tilemap tilemap;
    private InputMouse _inputMouse;
    private Vector3 _destination;
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
        _destination = transform.position;
        _inputMouse.Mouse.MouseClick.performed += empty => MouseClick();
    }

    private void MouseClick()
    {
        Vector2 mousePosition = _inputMouse.Mouse.MousePosition.ReadValue<Vector2>();
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        // Check for whether clicked position is a cell or not.
        Vector3Int gridPosition = tilemap.WorldToCell(mousePosition);
        if (tilemap.HasTile(gridPosition))
        {
            _destination = mousePosition;
        }
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, _destination) > 0.1f)
            transform.position = Vector3.MoveTowards(transform.position, _destination, 5 * Time.deltaTime);
    }
}
