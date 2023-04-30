using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "GroundTile", menuName = "Scriptable Tiles/Ground Tile")]
public class GroundTile : Tile
{
    private const float TileHalfHeight = 0.75f;
    private const float TileHalfWidth = 0.5f;
    private const int PossibleNeighborCount = 6; // Because it's hexagon.

    public bool IsWalkable = true;
    public bool IsEmpty = true;
    public int MoveCost = 1;

    public GroundTile Connection { get; private set; }
    public int GValue { get; private set; }
    public int HValue { get; private set; }
    public int FValue => GValue + HValue;
    public void SetConnection(GroundTile groundTile) => Connection = groundTile;
    public void SetGValue(int g) => GValue = g;
    public void SetHValue(int h) => HValue = h;

    public List<Vector3Int> GetNeighborPositions(Tilemap tilemap, Vector3 currentTilePosition)
    {
        List<Vector3Int> neighborPositions = new List<Vector3Int>();

        Vector3Int currentCoord = tilemap.WorldToCell(currentTilePosition);

        if (currentCoord.y % 2 == 0)
        {
            neighborPositions.Add(new Vector3Int( currentCoord.x - 1, currentCoord.y + 1 , 0));
            neighborPositions.Add(new Vector3Int( currentCoord.x, currentCoord.y + 1 , 0));
            neighborPositions.Add(new Vector3Int( currentCoord.x - 1, currentCoord.y, 0));
            neighborPositions.Add(new Vector3Int( currentCoord.x + 1, currentCoord.y, 0));
            neighborPositions.Add(new Vector3Int( currentCoord.x - 1, currentCoord.y - 1 , 0));
            neighborPositions.Add(new Vector3Int( currentCoord.x, currentCoord.y - 1 , 0));
        }
        else
        {
            neighborPositions.Add(new Vector3Int( currentCoord.x, currentCoord.y + 1 , 0));
            neighborPositions.Add(new Vector3Int( currentCoord.x + 1, currentCoord.y + 1 , 0));
            neighborPositions.Add(new Vector3Int( currentCoord.x - 1, currentCoord.y, 0));
            neighborPositions.Add(new Vector3Int( currentCoord.x + 1, currentCoord.y, 0));
            neighborPositions.Add(new Vector3Int( currentCoord.x, currentCoord.y - 1 , 0));
            neighborPositions.Add(new Vector3Int( currentCoord.x + 1, currentCoord.y - 1 , 0));
        }

        /*
        var neighborTileCoord =
            tilemap.WorldToCell(currentTilePosition + new Vector3(-TileHalfWidth, -TileHalfHeight, 0));
        if (tilemap.HasTile(neighborTileCoord) && tilemap.GetTile<GroundTile>(neighborTileCoord).IsWalkable)
        {
            neighborTiles.Add(tilemap.GetTile<GroundTile>(tilemap.WorldToCell(neighborTileCoord)));
        }

        neighborTileCoord = tilemap.WorldToCell(currentTilePosition + new Vector3(-TileHalfWidth, TileHalfHeight, 0));
        if (tilemap.HasTile(neighborTileCoord) && tilemap.GetTile<GroundTile>(neighborTileCoord).IsWalkable)
        {
            neighborTiles.Add(tilemap.GetTile<GroundTile>(tilemap.WorldToCell(neighborTileCoord)));
        }

        neighborTileCoord = tilemap.WorldToCell(currentTilePosition + new Vector3(-(TileHalfWidth * 2), 0, 0));
        if (tilemap.HasTile(neighborTileCoord) && tilemap.GetTile<GroundTile>(neighborTileCoord).IsWalkable)
        {
            neighborTiles.Add(tilemap.GetTile<GroundTile>(tilemap.WorldToCell(neighborTileCoord)));
        }

        neighborTileCoord = tilemap.WorldToCell(currentTilePosition + new Vector3(TileHalfWidth, -TileHalfHeight, 0));
        if (tilemap.HasTile(neighborTileCoord) && tilemap.GetTile<GroundTile>(neighborTileCoord).IsWalkable)
        {
            neighborTiles.Add(tilemap.GetTile<GroundTile>(tilemap.WorldToCell(neighborTileCoord)));
        }

        neighborTileCoord = tilemap.WorldToCell(currentTilePosition + new Vector3(TileHalfWidth, TileHalfHeight, 0));
        if (tilemap.HasTile(neighborTileCoord) && tilemap.GetTile<GroundTile>(neighborTileCoord).IsWalkable)
        {
            neighborTiles.Add(tilemap.GetTile<GroundTile>(tilemap.WorldToCell(neighborTileCoord)));
        }

        neighborTileCoord = tilemap.WorldToCell(currentTilePosition + new Vector3(TileHalfWidth * 2, 0, 0));
        if (tilemap.HasTile(neighborTileCoord) && tilemap.GetTile<GroundTile>(neighborTileCoord).IsWalkable)
        {
            
            neighborTiles.Add(tilemap.GetTile<GroundTile>(tilemap.WorldToCell(neighborTileCoord)));
        }
        */
        return neighborPositions;
    }
}