using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "GroundTile", menuName = "Scriptable Tiles/Ground Tile")]
public class GroundTile : Tile
{
    #region Fields

    public bool IsEmpty = true;
    public int MoveCost = 1;

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

