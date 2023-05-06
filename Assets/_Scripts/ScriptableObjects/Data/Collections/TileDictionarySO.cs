using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "Tile Dictionary")]
public class TileDictionarySO : ScriptableObject
{
    public List<HexTile> Tiles = new List<HexTile>();

    [Button()]
    public void DebugMe()
    {
        Debug.Log(Tiles[0].Coord);
        foreach (var VARIABLE in Tiles[0].Neighbors)
        {
            Debug.Log(VARIABLE.Coord);
        }
    }

    public HexTile GetTileFromDictionary(Vector3Int coord)
    {
        foreach (var t in Tiles)
        {
            if (t.Coord == coord)
                return t;
        }

        return null;
    }
}

[Serializable]
public class HexTile
{
    [NonSerialized] public List<HexTile> Neighbors;
    public HexTile(Vector3Int coord, bool isMovable)
    {
        Coord = coord;
        IsMovable = isMovable;
    }
    
    public Vector3Int Coord;
    public bool IsMovable;

    public HexTile Connection { get; private set; }
    public int GValue { get; private set; }
    public int HValue { get; private set; }
    public int FValue => GValue + HValue;
    public void SetConnection(HexTile hexTile) => Connection = hexTile;
    public void SetGValue(int g) => GValue = g;
    public void SetHValue(int h) => HValue = h;

    public int GetDistance(HexTile otherTile)
    {
        int moveCount = 0;
        Vector3Int tempCoord = Coord;
        Vector3Int destinationCoord = otherTile.Coord;
        while (tempCoord != destinationCoord)
        {
            Vector3Int move = new Vector3Int();
            if (tempCoord.y % 2 == 0)
            {
                if (tempCoord.y > destinationCoord.y)
                {
                    move.y--;
                    if (tempCoord.x > destinationCoord.x)
                        move.x--;
                }
                else if(tempCoord.y < destinationCoord.y)
                {
                    move.y++;
                    if (tempCoord.x > destinationCoord.x)
                        move.x--;
                }
                else
                {
                    if (tempCoord.x > destinationCoord.x)
                        move.x--;
                    else
                        move.x++;
                }
            }
            else
            {
                if (tempCoord.y > destinationCoord.y)
                {
                    move.y--;
                    if (tempCoord.x < destinationCoord.x)
                        move.x++;
                }
                else if(tempCoord.y < destinationCoord.y)
                {
                    move.y++;
                    if (tempCoord.x < destinationCoord.x)
                        move.x++;
                }
                else
                {
                    if (tempCoord.x < destinationCoord.x)
                        move.x++;
                    else
                        move.x--;
                }
            }

            tempCoord += move;
            moveCount++;
        }

        return moveCount;
    }
}