using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    [SerializeField] private StateSO _gameState;
    [SerializeField] private Tilemap _tilemap;
    [SerializeField] private MovementSystemSO _movementSystem;
    [SerializeField] private PathfindingSystemSO _pathfindingSystem;

    
    private void Awake()
    {
        _movementSystem.Init(_tilemap);
    }
}