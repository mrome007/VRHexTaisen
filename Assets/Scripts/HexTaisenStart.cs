using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTaisenStart : MonoBehaviour 
{
    [SerializeField]
    private TilesCreator tilesCreator;

    [SerializeField]
    private HexTaisenTurnsController turnsController;

    [SerializeField]
    private List<TaisenUnit> units;

    [SerializeField]
    private List<TaisenUnit> enemies;
    
    private void Start()
    {
        tilesCreator.CreateTiles();
        turnsController.StartHexTaisen();

        units.ForEach(unit => unit.SetOccupiedTile(tilesCreator.GetRandomTile()));
        enemies.ForEach(enemy => enemy.SetOccupiedTile(tilesCreator.GetRandomTile()));
    }
}
