using System.Collections.Generic;
using _Scripts.Data.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace _Scripts.Managers
{
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
                if (_tileDictionary.Tiles[i].Coord == movableAttributeChangeTilePos)
                    _tileDictionary.Tiles[i].IsMovable = !_tileDictionary.Tiles[i].IsMovable;
        }

        private void GetAllTiles()
        {
            BoundsInt bounds = _tilemap.cellBounds;

            // loop over the bounds (from min to max) on both axes
            for (int x = bounds.min.x; x < bounds.max.x; x++)
            for (int y = bounds.min.y; y < bounds.max.y; y++)
            {
                Vector3Int cellPosition = new Vector3Int(x, y, 0);

                GroundTile tile = _tilemap.GetTile<GroundTile>(cellPosition);
                Vector3Int coord = new(x, y, 0);

                if (tile == null) continue;
                bool isMovable = tile.IsMovable;

                _tileDictionary.Tiles.Add(new HexTile(coord, isMovable));
            }
        }

        private void SetNeighbors()
        {
            foreach (HexTile t in _tileDictionary.Tiles)
            {
                List<HexTile> neigbors = new();
                List<Vector3Int> neighborPositions = GetNeighborPositions(t.Coord);

                foreach (HexTile h in _tileDictionary.Tiles)
                    if (neighborPositions.Contains(h.Coord))
                        neigbors.Add(h);

                t.Neighbors = neigbors;
            }
        }

        public List<Vector3Int> GetNeighborPositions(Vector3Int tileCoord)
        {
            List<Vector3Int> neighborPositions = new();

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
}