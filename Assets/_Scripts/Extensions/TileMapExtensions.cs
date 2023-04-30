using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace _Scripts.Extensions
{
    public static class TileMapExtensions
    {
        /*
        public static IEnumerable<TileData> GetAllTiles(this Tilemap tilemap)
        {
            // note: optionally call tilemap.CompressBounds() some time prior to this
            var bounds = tilemap.cellBounds;

            // loop over the bounds (from min to max) on both axes
            for (int x = bounds.min.x; x < bounds.max.x; x++)
            {
                for (int y = bounds.min.y; y < bounds.max.y; y++)
                {
                    var cellPosition = new Vector3Int(x, y, 0);

                    
                    var tile = tilemap.GetTile(cellPosition);
                    var coord = new Vector2Int(x, y);

                    if (tile == null)
                    {
                        continue;
                    }

                    var tileData = new TileData(coord, tile);
                    yield return tileData;             
                }
            }
        }
        */
    }
/*
    public sealed class TileData
    {
        public TileData(
            Vector2Int coord,
            TileBase tile)
        {
            Coord = coord;
            Tile = tile;
        }
        
        public Vector2Int Coord { get; }

        public TileBase Tile { get; }
    }
*/
}