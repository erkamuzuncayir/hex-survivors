using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu (menuName = "Movement System")]
public class MovementSystemSO : ScriptableObject
{
    [SerializeField] private PathfindingSystemSO _pathfindingSystem;
    [SerializeField] private float _speed = 1f;

    public IEnumerator Move(Tilemap tilemap, GameObject mover, Vector3Int destinationCoord, Action<bool> isCompleted = null)
    {
        List<Vector3Int> moves =
            _pathfindingSystem.FindAllMoves(tilemap, tilemap.WorldToCell(mover.transform.position), destinationCoord);

        foreach (var m in moves)
            yield return ContinuousMove(mover, tilemap.CellToWorld(m));

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