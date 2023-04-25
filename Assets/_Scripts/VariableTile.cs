using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "tile_", menuName = "Tiles/Scriptable Tile")]
public class VariableTile : Tile
{
    #region Fields

    public bool IsEmpty = true;
    public int moveCost = 1;

    #endregion

    #region API

    #endregion

    #region Implementation



    #endregion

    #region Editor Only

#if UNITY_EDITOR
#endif

    #endregion
}

