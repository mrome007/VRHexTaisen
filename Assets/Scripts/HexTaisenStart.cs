using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTaisenStart : MonoBehaviour 
{
    [SerializeField]
    private TilesCreator tilesCreator;

    [SerializeField]
    private HexTaisenTurnsController turnsController;
    
    private void Start()
    {
        tilesCreator.CreateTiles();
        turnsController.StartHexTaisen();
    }
}
