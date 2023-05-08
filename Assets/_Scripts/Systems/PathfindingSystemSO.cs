using System.Collections.Generic;
using System.Linq;
using _Scripts.Data.Collections;
using UnityEngine;

namespace _Scripts.Systems
{
    [CreateAssetMenu(fileName = "Pathfinding System")]
    public class PathfindingSystemSO : ScriptableObject
    {
        public static List<HexTile> FindPath(HexTile startTile, HexTile targetTile)
        {
            List<HexTile> toSearch = new() { startTile };
            List<HexTile> processed = new();

            while (toSearch.Any())
            {
                HexTile current = toSearch[0];
                foreach (HexTile t in toSearch)
                    if (t.FValue < current.FValue || (t.FValue == current.FValue && t.HValue < current.HValue))
                        current = t;

                processed.Add(current);
                toSearch.Remove(current);

                if (current == targetTile)
                {
                    HexTile currentPathTile = targetTile;
                    List<HexTile> path = new();
                    while (currentPathTile != startTile)
                    {
                        path.Add(currentPathTile);
                        currentPathTile = currentPathTile.Connection;
                    }

                    return path;
                }

                foreach (HexTile neighbor in current.Neighbors.Where(t => t.IsMovable && !processed.Contains(t)))
                {
                    bool inSearch = toSearch.Contains(neighbor);

                    int costToNeighbor = current.GValue + current.GetDistance(neighbor);

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
}