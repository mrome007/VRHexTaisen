using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaisenUnit : MonoBehaviour 
{
    public event EventHandler UnitDeath;

    [SerializeField]
    private int health;

    public int Health 
    { 
        get
        { 
            return health; 
        }
    }

    public HexTile OccupiedTile { get; private set; }

    public void ApplyDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            PostDeath();
        }
    }

    private void PostDeath()
    {
        var handler = UnitDeath;
        if(handler != null)
        {
            handler(this, null);
        }
    }

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
