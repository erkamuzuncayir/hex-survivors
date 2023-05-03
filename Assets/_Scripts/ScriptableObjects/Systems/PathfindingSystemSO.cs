using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu (fileName = "PathfindingSystem")]
public class PathfindingSystemSO : ScriptableObject
{
    private Tilemap _tilemap;

    public void Init(Tilemap tilemap)
    {
        _tilemap = tilemap;
    }
    
    public List<Vector3Int> FindAllMoves(Vector3Int currentCoord, Vector3Int destinationCoord)
    {
        List<Vector3Int> moves = new List<Vector3Int>();
        while (currentCoord != destinationCoord)
        {
            Vector3Int move = new Vector3Int();
            if (currentCoord.y % 2 == 0)
            {
                if (currentCoord.y > destinationCoord.y)
                {
                    if(IsMovePossible(currentCoord + move + new Vector3Int(0,-1,0)))
                        move.y--;
                    if (currentCoord.x > destinationCoord.x)
                    {
                        if(IsMovePossible(currentCoord + move + new Vector3Int(-1,0,0)))
                            move.x--;
                    }
                }
                else if(currentCoord.y < destinationCoord.y)
                {
                    if(IsMovePossible(currentCoord + move + new Vector3Int(0,1,0)))
                        move.y++;
                    if (currentCoord.x > destinationCoord.x)
                    {
                        if(IsMovePossible(currentCoord + move + new Vector3Int(-1,0,0))) 
                            move.x--;
                    }
                }
                else
                {
                    if (currentCoord.x > destinationCoord.x)
                    {
                        if(IsMovePossible(currentCoord + move + new Vector3Int(-1,0,0)))
                            move.x--;
                    }
                    else
                    {
                        if(IsMovePossible(currentCoord + move + new Vector3Int(1,0,0)))
                            move.x++;
                    }
                }
            }
            else
            {
                if (currentCoord.y > destinationCoord.y)
                {
                    if(IsMovePossible(currentCoord + move + new Vector3Int(0,-1,0)))
                        move.y--;
                    if (currentCoord.x < destinationCoord.x)
                    {
                        if(IsMovePossible(currentCoord + move + new Vector3Int(1,0,0)))
                            move.x++;
                    }
                }
                else if(currentCoord.y < destinationCoord.y)
                {
                    if(IsMovePossible(currentCoord + move + new Vector3Int(0,1,0)))
                        move.y++;
                    if (currentCoord.x < destinationCoord.x)
                    {
                        if(IsMovePossible(currentCoord + move + new Vector3Int(1,0,0)))
                            move.x++;
                    }
                }
                else
                {
                    if (currentCoord.x < destinationCoord.x)
                    {
                        if(IsMovePossible(currentCoord + move + new Vector3Int(1,0,0)))
                            move.x++;
                    }
                    else
                    {
                        if(IsMovePossible(currentCoord + move + new Vector3Int(1,0,0)))
                            move.x++;
                    }
                }
            }

            currentCoord += move;
            moves.Add(currentCoord);
        }

        return moves;
    }

    private bool IsMovePossible(Vector3Int moveCoord)
    {
        return _tilemap.HasTile(moveCoord) && _tilemap.GetTile<GroundTile>(moveCoord).IsWalkable;
    }
}