using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Data.Collections;
using _Scripts.Data.RuntimeSets;
using _Scripts.Events;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace _Scripts.Systems
{
    [CreateAssetMenu(menuName = "Movement System")]
    public class MovementSystemSO : ScriptableObject
    {
        [SerializeField] private TileDictionarySO _tileDictionary;
        [SerializeField] private Vector3EventSO _movableAttributeChangeTilePos;
        [SerializeField] private GameObjectRuntimeSet _player;
   
        private Tilemap _tilemap;

        [SerializeField] private float _speed = 1f;

        public void Init(Tilemap tilemap)
        {
            _tilemap = tilemap;
        }

        public IEnumerator MovePlayer(Vector3Int destinationCoord, Action<bool> isCompleted = null)
        {
            Vector3Int playerCoord = _tilemap.WorldToCell(_player.Items[0].transform.position);

            
            Debug.Log(_tileDictionary.GetTileFromDictionary(playerCoord).IsMovable);
            List<HexTile> tilePath = PathfindingSystemSO.FindPath(_tileDictionary.GetTileFromDictionary(playerCoord),
                _tileDictionary.GetTileFromDictionary(destinationCoord));
            List<Vector3Int> moves = tilePath.Select(t => t.Coord).ToList();
            moves.Reverse();

            for (int i = 0; i < moves.Count; i++)
            {
                if (i == 0)
                {
                    _movableAttributeChangeTilePos.Raise(playerCoord);
                    _movableAttributeChangeTilePos.Raise(moves[0]);
                }
                else if(i == moves.Count - 1)
                {
                    _movableAttributeChangeTilePos.Raise(moves[i - 1]);
                }
                else
                {
                    _movableAttributeChangeTilePos.Raise(moves[i - 1]);
                    _movableAttributeChangeTilePos.Raise(moves[i]);
                }

                yield return ContinuousMove(_player.Items[0], _tilemap.CellToWorld(moves[i]));
            }

            isCompleted?.Invoke(true);
        }


        public IEnumerator MoveEnemy(GameObject mover, Action<bool> isCompleted = null)
        {
            Vector3Int playerCoord = _tilemap.WorldToCell(_player.Items[0].transform.position);
            Vector3Int moverCoord = _tilemap.WorldToCell(mover.transform.position);

            Debug.Log(_tileDictionary.GetTileFromDictionary(moverCoord).IsMovable);
            
            
            List<HexTile> tilePath = PathfindingSystemSO.FindPath(_tileDictionary.GetTileFromDictionary(moverCoord),
                _tileDictionary.GetTileFromDictionary(playerCoord));
            List<Vector3Int> moves = tilePath.Select(t => t.Coord).ToList();
            moves.Reverse();

            for (int i = 0; i < moves.Count; i++)
            {
                if (i == 0)
                {
                    _movableAttributeChangeTilePos.Raise(moverCoord);
                    _movableAttributeChangeTilePos.Raise(moves[0]);
                }
                else
                {
                    _movableAttributeChangeTilePos.Raise(moves[i - 1]);
                    _movableAttributeChangeTilePos.Raise(moves[i]);
                }

                yield return ContinuousMove(mover, _tilemap.CellToWorld(moves[i]));
            }

            isCompleted?.Invoke(true);
        }


        private IEnumerator ContinuousMove(GameObject mover, Vector3 end)
        {
            float t = 0;
            while (t < 1)
            {
                mover.transform.position = Vector3.MoveTowards(mover.transform.position, end, t);
                t = t + Time.deltaTime / _speed;
                yield return new WaitForEndOfFrame();
            }
        }
    }
}