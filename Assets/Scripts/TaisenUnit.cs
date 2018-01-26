﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaisenUnit : MonoBehaviour 
{
    public HexTile OccupiedTile { get; set; }

    public void SetOccupiedTile(HexTile tile)
    {
        OccupiedTile = tile;
        transform.position = new Vector3(tile.transform.position.x, 1.65f, tile.transform.position.z);
    }
}
