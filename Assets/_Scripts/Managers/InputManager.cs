using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Tilemap _tilemap;
    [SerializeField] private TileDictionarySO _tileDictionary;
    [SerializeField] private GameObjectEvent _selectedGameObject;
    [SerializeField] private StateSO _gameState;
    [SerializeField] private Vector2Event _playerPositionAnnouncer;
    [SerializeField] private Vector2Event _destinationPositionAnnouncer;
    [SerializeField] private Vector3SO _playerPosition;
    
    private InputMouse _inputMouse;
    private Camera _mainCamera;
    private bool _isPlayerSelected = false;
    
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
        Vector3Int targetCoord = _tilemap.WorldToCell(mousePosition);
        
        if (_gameState.GameState == StateSO.State.PlayerTurn)
        {
            if(hit.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                _selectedGameObject.Raise(hit.collider.gameObject);
                _isPlayerSelected = true;
                _playerPositionAnnouncer.Raise(mousePosition);
            }
            else if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                // For clicking and investigating enemy attributes
            }
            else if(!(_tilemap.HasTile(targetCoord) && _tileDictionary.GetTileFromDictionary(targetCoord).IsMovable))
            {
                /*
                 * If an enemy already selected return to non-selected situation.
                 * if(enemySelected bla bla )
                 */
                if (_isPlayerSelected)
                    _isPlayerSelected = false;
            }
            else if(_isPlayerSelected)
            {
                _destinationPositionAnnouncer.Raise(mousePosition);
                _isPlayerSelected = false;
                
            }
        }
    }

}
