using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "Tile Dictionary")]
public class TileDictionarySO : ScriptableObject
{
    public List<TileData> Tiles = new List<TileData>();
}

[Serializable]
public class TileData
{
    public TileData(GroundTile tile, Vector2Int coord)
    {
        Tile = tile;
        TileCoord = coord;
    }
    
    public GroundTile Tile;
    public Vector2Int TileCoord;
}