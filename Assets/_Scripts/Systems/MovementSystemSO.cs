using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Actors;
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
        [SerializeField] private GameObjectRuntimeSet _playerRuntimeSet;
        private Player _playerScript;
        private Tilemap _tilemap;

        [SerializeField] private float _speed = 1f;

        public void Init(Tilemap tilemap)
        {
            _tilemap = tilemap;
        }

        public IEnumerator MovePlayer(Vector3Int destinationCoord, int moveRange, Action<bool> isCompleted)
        {
            Vector3Int playerCoord = _tilemap.WorldToCell(_playerRuntimeSet.Items[0].transform.position);

            List<HexTile> tilePath = Pathfinding.FindPath(_tileDictionary.GetTileFromDictionary(playerCoord),
                _tileDictionary.GetTileFromDictionary(destinationCoord), _playerRuntimeSet.Items[0].GetComponent<Player>().AttackRange);

            List<Vector3Int> moves = new ();
            for(int i = tilePath.Count - 1; i >= 0; i--)
                moves.Add(tilePath[i].Coord);

            int moveCount = (moveRange < moves.Count) ? moveRange : moves.Count;
            
            _movableAttributeChangeTilePos.Raise(playerCoord);
            _movableAttributeChangeTilePos.Raise(moves[moveCount-1]);
            
            for (int i = 0; i < moveCount; i++)
                yield return ContinuousMove(_playerRuntimeSet.Items[0], _tilemap.CellToWorld(moves[i]));

            isCompleted.Invoke(true);
        }
        
        public IEnumerator MoveEnemy(GameObject mover, int moveRange, Action<bool> isCompleted)
        {
            Vector3Int playerCoord = _tilemap.WorldToCell(_playerRuntimeSet.Items[0].transform.position);
            Vector3Int moverCoord = _tilemap.WorldToCell(mover.transform.position);
            
            
            List<HexTile> tilePath = Pathfinding.FindPath(_tileDictionary.GetTileFromDictionary(moverCoord),
                _tileDictionary.GetTileFromDictionary(playerCoord), mover.GetComponent<Enemy>().AttackRange);
            
            List<Vector3Int> moves = new ();
            for(int i = tilePath.Count - 1; i >= 0; i--)
               moves.Add(tilePath[i].Coord);
            
            int moveCount = (moveRange < moves.Count) ? moveRange : moves.Count;
            
            _movableAttributeChangeTilePos.Raise(moverCoord);
            _movableAttributeChangeTilePos.Raise(moves[moveCount-1]);

            for (int i = 0; i < moveCount; i++)
                yield return ContinuousMove(mover, _tilemap.CellToWorld(moves[i]));

            isCompleted.Invoke(true);
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