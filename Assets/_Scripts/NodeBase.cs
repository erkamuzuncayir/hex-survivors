using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Tilemaps;

// TODO: 2 KENARI KONTROL ETTIN. 6 KENARA TAMAMLA VE PATHFINDINGTEN DEVAM ET.
public class NodeBase : GroundTile
{
    /*
    public Tile tile;
    public NodeBase Connection { get; private set; }
    public float GValue { get; private set; }
    public float HValue { get; private set; }
    public float FValue => GValue + HValue;

    public void SetConnection(NodeBase nodeBase) => Connection = nodeBase;

    public void SetGValue(float g) => GValue = g;
    public void SetHValue(float h) => HValue = h;
    public List<NodeBase> FindNeighbors(Tilemap map, NodeBase current)
    {
        List<NodeBase> neighbors = new List<NodeBase>();

        Vector3Int currentGridCoord = map.WorldToCell(transform.GetPosition());
        
        currentGridCoord.x++;
        if (map.HasTile(currentGridCoord))
        {
            NodeBase tempNeighbor = ScriptableObject.CreateInstance<NodeBase>();
            tempNeighbor.tile = map.GetTile<GroundTile>(currentGridCoord);
            neighbors.Add(tempNeighbor);
        }

        currentGridCoord.y++;
        if (map.HasTile(currentGridCoord))
        {
            NodeBase tempNeighbor = ScriptableObject.CreateInstance<NodeBase>();
            tempNeighbor.tile = map.GetTile<GroundTile>(currentGridCoord);
            neighbors.Add(tempNeighbor);
        }
        
        return neighbors;
    }
    */

}
