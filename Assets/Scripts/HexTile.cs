using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTile : MonoBehaviour 
{
    public List<HexTile> AdjacentTiles;

    private void Awake()
    {
        AdjacentTiles = new List<HexTile>();
    }
}
