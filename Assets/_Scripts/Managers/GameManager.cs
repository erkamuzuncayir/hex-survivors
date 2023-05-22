using _Scripts.Systems;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace _Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameStateSystemSO _gameStateSystem;
        [SerializeField] private Tilemap _tilemap;
        [SerializeField] private MovementSystemSO _movementSystem;


        private void Awake()
        {
            _gameStateSystem.GameState = State.PlayerTurn;
            _movementSystem.Init(_tilemap);
        }
    }
}