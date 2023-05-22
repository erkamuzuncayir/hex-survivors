using _Scripts.Data.Collections;
using _Scripts.Data.RuntimeSets;
using _Scripts.Data.Type;
using _Scripts.Events;
using _Scripts.Systems;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace _Scripts.Actors
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private TileDictionarySO _tileDictionary;
        [SerializeField] private GameObjectEventSO _deathAnnouncer;
        [SerializeField] private VoidEventSO _turnCompleteAnnouncer;
        [SerializeField] private MovementSystemSO _movementSystem;
        [SerializeField] private GameObjectRuntimeSet _enemies;
        [SerializeField] private EnemyRuntimeSet _enemyScripts;
        private Vector3Int _destinationCoordinate;

        private bool _isMoving;

        [SerializeField] private int _moveRange = 2;
        public int AttackRange;
        [SerializeField] private int _damage;
        [SerializeField] private int _health;
        [SerializeField] private AnimatorController _animatorController;

        //ENEMYİ YARAT ARTIK. AKSİYONLARINI VE METHODLARINI EVENTLERİ FALAN
        private void OnEnable()
        {
            _enemyScripts.AddToList(this);
            _enemies.AddToList(gameObject);
        }

        private void OnDisable()
        {
            _enemyScripts.RemoveFromList(this);
            _enemies.RemoveFromList(gameObject);
        }

        public void OnEnemyTurn()
        {
            // TODO: If player isn't attack range.
            MoveAction();
        }

        public void OnDeath()
        {
            _deathAnnouncer.Raise(gameObject);
        }

        public void MoveAction()
        {
            // Check for whether clicked position is a cell or not.
            if (!_isMoving)
            {
                _isMoving = true;
                StartCoroutine(_movementSystem.MoveEnemy(gameObject, _moveRange,
                    isOperationCompleted =>
                    {
                        if (isOperationCompleted)
                        {
                            _isMoving = false;
                            _turnCompleteAnnouncer.Raise();
                        }
                    }));
            }
        }
    }
}