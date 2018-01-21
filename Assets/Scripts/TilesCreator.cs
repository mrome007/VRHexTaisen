using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesCreator : MonoBehaviour 
{
    [SerializeField]
    private HexTile tileObject;

    [SerializeField]
    private int row;

    [SerializeField]
    private int column;

    private HexTile[,] tileGrid;
    private float startingXPosition = -7f;
    private float xPosition = -7f;
    private float xPositionIncrement = 1.75f;

    private float zRowPosition = 0f;
    private float zRowPositionIncrement = 1.5f;

    private void Awake()
    {
        tileGrid = new HexTile[column * 2, row];
        for(int rowIndex = 0; rowIndex < row; rowIndex++)
        {
            for(int columnIndex = 0; columnIndex < column * 2; columnIndex++)
            {
                tileGrid[columnIndex, rowIndex] = null;
            }
        }
    }

    private void Start()
    {
        CreateTileGrid();
    }

    private void CreateTileGrid()
    {
        var parentObject = new GameObject("TileGridParent");
        int tileCount = 1;
        for(int rowIndex = 0; rowIndex < row; rowIndex++, zRowPosition += zRowPositionIncrement)
        {
            var rowParent = new GameObject("TileRow" + rowIndex);
            var even = rowIndex % 2 == 0;

            for(int columnIndex = even ? 0 : 1; columnIndex < tileGrid.GetLength(0); columnIndex += 2, xPosition += xPositionIncrement)
            {
                var tile = Instantiate(tileObject, new Vector3(xPosition, 0f, 0f), Quaternion.identity);
                tile.name += tileCount; 
                tile.transform.parent = rowParent.transform;
                tileGrid[columnIndex, rowIndex] = tile;
                tileCount++;
            }

            rowParent.transform.position = new Vector3(even ? 0f : 0.85f, 0f, zRowPosition);
            rowParent.transform.parent = parentObject.transform;
            xPosition = startingXPosition;
        }

    }
}
