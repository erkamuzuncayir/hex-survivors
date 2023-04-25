using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    /*
    public static List<NodeBase> FindPath(NodeBase startNode, NodeBase targetNode)
    {
        var toSearch = new List<NodeBase>() { startNode };
        var processed = new List<NodeBase>();

        while (toSearch.Any())
        {
            var currentNode = toSearch[0];
            foreach (var t in toSearch)
                if (t.FValue < currentNode.FValue ||
                    t.FValue == currentNode.FValue &&
                    t.HValue < currentNode.HValue)
                    currentNode = t;
                
            processed.Add(currentNode);
            toSearch.Remove(currentNode);


        }
    }
    */
}
