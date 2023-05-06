using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Extensions;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    [SerializeField] private TileDictionarySO _tileDictionary;
    [SerializeField] private Tilemap _tilemap;
    
    private void Awake()
    {
        _tileDictionary.Tiles.Clear();
        
        GetAllTiles();
        SetNeighbors();
    }
    
    public void RevertMovableAttribute(Vector3 movableAttributeChangeTilePos)
    {
        for (int i = 0; i < _tileDictionary.Tiles.Count; i++)
        {
            if (_tileDictionary.Tiles[i].Coord == movableAttributeChangeTilePos)
            {
                
                _tileDictionary.Tiles[i].IsMovable = !_tileDictionary.Tiles[i].IsMovable;
            }
        }
    }
    
    private void GetAllTiles()
    {
        var bounds = _tilemap.cellBounds;

        // loop over the bounds (from min to max) on both axes
        for (int x = bounds.min.x; x < bounds.max.x; x++)
        {
            for (int y = bounds.min.y; y < bounds.max.y; y++)
            {
                var cellPosition = new Vector3Int(x, y, 0);
                
                GroundTile tile = _tilemap.GetTile<GroundTile>(cellPosition);
                Vector3Int coord = new Vector3Int(x, y, 0);

                if (tile == null)
                {
                    continue;
                }
                bool isMovable = tile.IsMovable;

                _tileDictionary.Tiles.Add(new HexTile(coord, isMovable));
            }
        }
    }

    private void SetNeighbors()
    {
        foreach (var t in _tileDictionary.Tiles)
        {
            List<HexTile> neigbors = new List<HexTile>();
            List<Vector3Int> neighborPositions = GetNeighborPositions(t.Coord);

            foreach (var h in _tileDictionary.Tiles)
            {
                if(neighborPositions.Contains(h.Coord))
                    neigbors.Add(h);
            }

            t.Neighbors = neigbors;
        }
    }
    
    public List<Vector3Int> GetNeighborPositions(Vector3Int tileCoord)
    {
        List<Vector3Int> neighborPositions = new List<Vector3Int>();
        
        if (tileCoord.y % 2 == 0)
        {
            neighborPositions.Add(new Vector3Int(tileCoord.x - 1, tileCoord.y + 1, 0));
            neighborPositions.Add(new Vector3Int(tileCoord.x, tileCoord.y + 1, 0));
            neighborPositions.Add(new Vector3Int(tileCoord.x - 1, tileCoord.y, 0));
            neighborPositions.Add(new Vector3Int(tileCoord.x + 1, tileCoord.y, 0));
            neighborPositions.Add(new Vector3Int(tileCoord.x - 1, tileCoord.y - 1, 0));
            neighborPositions.Add(new Vector3Int(tileCoord.x, tileCoord.y - 1, 0));
        }
        else
        {
            neighborPositions.Add(new Vector3Int(tileCoord.x, tileCoord.y + 1, 0));
            neighborPositions.Add(new Vector3Int(tileCoord.x + 1, tileCoord.y + 1, 0));
            neighborPositions.Add(new Vector3Int(tileCoord.x - 1, tileCoord.y, 0));
            neighborPositions.Add(new Vector3Int(tileCoord.x + 1, tileCoord.y, 0));
            neighborPositions.Add(new Vector3Int(tileCoord.x, tileCoord.y - 1, 0));
            neighborPositions.Add(new Vector3Int(tileCoord.x + 1, tileCoord.y - 1, 0));
        }

        return neighborPositions;
    }
}