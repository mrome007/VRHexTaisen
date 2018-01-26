using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaisenUnit : MonoBehaviour 
{
    public HexTile OccupiedTile { get; set; }

    public void SetOccupiedTile(HexTile tile)
    {
        OccupiedTile = tile;
        MoveTaisenUnit(tile.transform);
    }

    private void MoveTaisenUnit(Transform tilePos)
    {
        transform.position = new Vector3(tilePos.position.x, transform.position.y, tilePos.position.z);
    }
}
