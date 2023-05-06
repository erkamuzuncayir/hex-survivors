using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "GroundTile", menuName = "Scriptable Tiles/Ground Tile")]
public class GroundTile : Tile
{
    private const float TileHalfHeight = 0.75f;
    private const float TileHalfWidth = 0.5f;
    private const int PossibleNeighborCount = 6; // Because it's hexagon.

    public bool IsMovable = true;
}