using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private Vector2Event _playerPositionAnnouncer;
    private InputMouse _inputMouse;
    
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
        if (Camera.main != null) 
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Debug.Log(mousePosition);
        
        _playerPositionAnnouncer.Raise(mousePosition);
    }

}
