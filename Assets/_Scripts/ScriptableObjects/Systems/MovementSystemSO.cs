using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu (menuName = "Movement System")]
public class MovementSystemSO : ScriptableObject
{
    [SerializeField] private TileDictionarySO _tileDictionary;
    [SerializeField] private Vector3Event _movableAttributeChangeTilePos;
    [SerializeField] private PathfindingSystemSO _pathfindingSystem;
    [SerializeField] private GameObjectRuntimeSet _player;
    [SerializeField] private GameObjectRuntimeSet _enemies;
    
    private Tilemap _tilemap;
    
    [SerializeField] private float _speed = 1f;
    
    public void Init(Tilemap tilemap)
    {
        _tilemap = tilemap;
    }
    
    public IEnumerator MovePlayer(Vector3Int destinationCoord, Action<bool> isCompleted = null)
    {
        Vector3Int playerCoord = _tilemap.WorldToCell(_player.items[0].transform.position);        
        List<HexTile> tilePath = PathfindingSystemSO.FindPath(_tileDictionary.GetTileFromDictionary(playerCoord), _tileDictionary.GetTileFromDictionary(destinationCoord));

        List<Vector3Int> moves = tilePath.Select(t => t.Coord).ToList();
        moves.Reverse();

        for (var i = 0; i < moves.Count; i++)
        {
            if (i == 0)
            {
                _movableAttributeChangeTilePos.Raise(playerCoord);
                _movableAttributeChangeTilePos.Raise(moves[0]);
            }
            else
            {
                _movableAttributeChangeTilePos.Raise(moves[i-1]);
                _movableAttributeChangeTilePos.Raise(moves[i]);
            }
            yield return ContinuousMove(_player.items[0], _tilemap.CellToWorld(moves[i]));
        }

        isCompleted(true);
    }
    
        /*
    public IEnumerator MoveEnemy(GameObject mover, Action<bool> isCompleted = null)
    {
        Vector3Int playerCoord = _tilemap.WorldToCell(_player.items[0].transform.position);
        List<HexTile> moves =
            _pathfindingSystem.FindPath(_tilemap.WorldToCell(mover.transform.position), playerCoord);

        for (var i = 0; i < moves.Count; i++)
        {
            if (i == 0)
            {
                _movableAttributeChangeTilePos.Raise(_tilemap.WorldToCell(mover.transform.position));
                _movableAttributeChangeTilePos.Raise(moves[0]);
            }
            else
            {
                _movableAttributeChangeTilePos.Raise(moves[i-1]);
                _movableAttributeChangeTilePos.Raise(moves[i]);
            }
            yield return ContinuousMove(mover, _tilemap.CellToWorld(moves[i]));
        }
        isCompleted(true);
    }
*/

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