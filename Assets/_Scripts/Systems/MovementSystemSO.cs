using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<bool> MovePlayer(Vector3Int destinationCoord, int moveRange)
        {
            Vector3Int playerCoord = _tilemap.WorldToCell(_playerRuntimeSet.Items[0].transform.position);

            List<HexTile> tilePath = Pathfinding.FindPath(_tileDictionary.GetTileFromDictionary(playerCoord),
                _tileDictionary.GetTileFromDictionary(destinationCoord));

            List<Vector3Int> moves = new ();
            for(int i = tilePath.Count - 1; i >= 0; i--)
                moves.Add(tilePath[i].Coord);

            int moveCount = (moveRange < moves.Count) ? moveRange : moves.Count;
            
            _movableAttributeChangeTilePos.Raise(playerCoord);
            _movableAttributeChangeTilePos.Raise(moves[moveCount-1]);

            for (int i = 0; i < moveCount; i++)
            {
               await ContinuousMove(_playerRuntimeSet.Items[0], _tilemap.CellToWorld(moves[i]));
            }
            
            return true;
        }
        
        public async Task<bool> MoveEnemy(GameObject mover, int moveRange, Action<Vector3Int> newCoord)
        {
            Vector3Int playerCoord = _tilemap.WorldToCell(_playerRuntimeSet.Items[0].transform.position);
            Vector3Int moverCoord = _tilemap.WorldToCell(mover.transform.position);
            
            
            List<HexTile> tilePath = Pathfinding.FindPath(_tileDictionary.GetTileFromDictionary(moverCoord),
                _tileDictionary.GetTileFromDictionary(playerCoord));
            
            List<Vector3Int> moves = new ();
            for(int i = tilePath.Count - 1; i >= 0; i--)
               moves.Add(tilePath[i].Coord);

            if (moves.Count > 0)
            {
                int moveCount = (moveRange < moves.Count) ? moveRange : moves.Count;
                
                _movableAttributeChangeTilePos.Raise(moverCoord);
                _movableAttributeChangeTilePos.Raise(moves[moveCount-1]);
                newCoord(moves[moveCount - 1]);

                for (int i = 0; i < moveCount; i++)
                    await ContinuousMove(mover, _tilemap.CellToWorld(moves[i]));
            }

            return true;
        }
        
        private async Task<bool> ContinuousMove(GameObject mover, Vector3 end)
        {
            float t = 0;
            while (t < 1)
            {
                mover.transform.position = Vector3.MoveTowards(mover.transform.position, end, t);
                t = t + Time.deltaTime / _speed;
                await Task.Yield();
            }

            return true;
        }
        
        
    }
}