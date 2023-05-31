using System.Collections.Generic;
using System.Linq;
using _Scripts.Data.Collections;
using UnityEngine;

namespace _Scripts.Systems
{
    public static class Pathfinding
    {
        public static List<HexTile> FindPath(HexTile startTile, HexTile targetTile)
        {
            List<HexTile> path = SearchPath(startTile, targetTile);
         
            if (path == null)
            {
                List<HexTile> toSearchNeighbors = new() { targetTile };
                List<HexTile> processedNeighbors = new();
                List<HexTile> alternativeTargets = new List<HexTile>();
                alternativeTargets.AddRange(targetTile.Neighbors);
                HexTile alternateTarget;
                
                while (path == null)
                {
                    if (alternativeTargets.Count < 1)
                    {
                        alternativeTargets.AddRange(processedNeighbors[0].Neighbors);
                        processedNeighbors.RemoveAt(0);
                    }
                    
                    HexTile nearestNeighbor = alternativeTargets[0];
                    for (int i = 1; i < alternativeTargets.Count; i++)
                    {
                        if (alternativeTargets[i].FValue < nearestNeighbor.FValue)
                            nearestNeighbor = alternativeTargets[i];
                    }

                    alternateTarget = nearestNeighbor;
                    toSearchNeighbors.Add(nearestNeighbor);
                    processedNeighbors.Add(nearestNeighbor);
                    alternativeTargets.Remove(nearestNeighbor);
                    path = SearchPath(startTile, alternateTarget);
                }
            }
            
            return path;
        }

        private static List<HexTile> SearchPath(HexTile startTile, HexTile targetTile)
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
                    List<HexTile> path = new ();
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
                            /*
                            if (neighbor.HValue < startTile.GetDistance(targetTile))
                            {
                                HexTile currentPathTile = neighbor;
                                List<HexTile> path = new ();
                                while (currentPathTile != startTile)
                                {
                                    path.Add(currentPathTile);
                                    currentPathTile = currentPathTile.Connection;
                                }

                                return path;
                            }
*/
                            toSearch.Add(neighbor);
                        }
                    }
                }
                
            }


            return null;
        }
    }
}