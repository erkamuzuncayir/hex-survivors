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
        
        GetAllTiles(_tilemap);
    }
    
    public void GetAllTiles(Tilemap tilemap)
    {
        var bounds = tilemap.cellBounds;

        // loop over the bounds (from min to max) on both axes
        for (int x = bounds.min.x; x < bounds.max.x; x++)
        {
            for (int y = bounds.min.y; y < bounds.max.y; y++)
            {
                var cellPosition = new Vector3Int(x, y, 0);
                
                var tile = tilemap.GetTile<GroundTile>(cellPosition);
                var coord = new Vector2Int(x, y);

                if (tile == null)
                {
                    continue;
                }

                _tileDictionary.Tiles.Add(new TileData(tile, coord));
            }
        }
    }
}