using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class InputManager : MonoBehaviour
{
    [SerializeField] private StateSO _gameState;
    [SerializeField] private Vector2Event _playerPositionAnnouncer;
    [SerializeField] private Vector2Event _destinationPositionAnnouncer;
    [SerializeField] private Vector3SO _playerPosition;
    
    private InputMouse _inputMouse;
    private Camera _mainCamera;
    
    private void Awake()
    {
        _inputMouse = new InputMouse();
        _mainCamera = Camera.main;
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
        
        Ray ray = _mainCamera.ScreenPointToRay(mousePosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);
        
        // Turns screen space to world space
        mousePosition = _mainCamera.ScreenToWorldPoint(mousePosition);
        if (hit.collider != null && hit.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            _playerPosition.value = mousePosition; 
            _playerPositionAnnouncer.Raise(mousePosition);
        }
        else
        {
            _destinationPositionAnnouncer.Raise(mousePosition);
        }
    }

}
