using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu (menuName = "Movement System")]
public class MovementSystemSO : ScriptableObject
{
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
        GameObject player = _player.items[0];
        List<Vector3Int> moves =
            _pathfindingSystem.FindAllMoves(_tilemap.WorldToCell(player.transform.position), destinationCoord);

        foreach (var m in moves)
            yield return ContinuousMove(player, _tilemap.CellToWorld(m));
        
        isCompleted(true);
    }
    
    public IEnumerator MoveEnemy(GameObject mover, Action<bool> isCompleted = null)
    {
        Vector3Int playerCoord = _tilemap.WorldToCell(_player.items[0].transform.position);
        
        List<Vector3Int> moves =
            _pathfindingSystem.FindAllMoves(_tilemap.WorldToCell(mover.transform.position), playerCoord);

        foreach (var m in moves)
            yield return ContinuousMove(mover, _tilemap.CellToWorld(m));

        isCompleted(true);
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