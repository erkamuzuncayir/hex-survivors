using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu (fileName = "PathfindingSystem")]
public class PathfindingSystemSO : ScriptableObject
{
    [SerializeField] private GameObjectRuntimeSet _enemies;

    public static List<HexTile> FindPath(HexTile startTile, HexTile targetTile)
    {
        List<HexTile> toSearch = new List<HexTile>() { startTile };
        List<HexTile> processed = new List<HexTile>();
        
        while (toSearch.Any())
        {
            HexTile current = toSearch[0];
            foreach (var t in toSearch)
            {
                if (t.FValue < current.FValue || t.FValue == current.FValue && t.HValue < current.HValue)
                    current = t;
            }
            
            processed.Add(current);
            toSearch.Remove(current);

            if (current == targetTile)
            {
                HexTile currentPathTile = targetTile;
                List<HexTile> path = new List<HexTile>();
                while (currentPathTile != startTile)
                {
                    path.Add(currentPathTile);
                    currentPathTile = currentPathTile.Connection;
                }

                return path;
            }
            
            foreach (var neighbor in current.Neighbors.Where(t => t.IsMovable && !processed.Contains(t)))
            {
                bool inSearch = toSearch.Contains(neighbor);

                var costToNeighbor = current.GValue + current.GetDistance(neighbor);

                if (!inSearch || costToNeighbor < neighbor.GValue)
                {
                    neighbor.SetGValue(costToNeighbor);
                    neighbor.SetConnection(current);

                    if (!inSearch)
                    {
                        neighbor.SetHValue(neighbor.GetDistance(targetTile));
                        toSearch.Add(neighbor);
                    }
                }
            }
        }
        return null;
    }
}