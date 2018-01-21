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
        CreateHexTileGrid();
        ConnectHexTileGrid();
    }

    private void CreateHexTileGrid()
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

    private void ConnectHexTileGrid()
    {
        for(int rowIndex = 0; rowIndex < row; rowIndex++, zRowPosition += zRowPositionIncrement)
        {
            var even = rowIndex % 2 == 0;
            for(int columnIndex = even ? 0 : 1; columnIndex < tileGrid.GetLength(0); columnIndex += 2, xPosition += xPositionIncrement)
            {
                ConnectHexTile(columnIndex, rowIndex, tileGrid[columnIndex, rowIndex]);
            }
        }
    }

    private void ConnectHexTile(int currentXPos, int currentYPos, HexTile tile)
    {
        var topLeft = new Vector2Int(currentXPos - 1, currentYPos + 1);
        var left = new Vector2Int(currentXPos - 2, currentYPos);
        var bottomLeft = new Vector2Int(currentXPos - 1, currentYPos - 1);
        var bottomRight = new Vector2Int(currentXPos + 1, currentYPos - 1);
        var right = new Vector2Int(currentXPos + 2, currentYPos);
        var topRight = new Vector2Int(currentXPos + 1, currentYPos + 1);

        ConnectTile(topLeft, tile);
        ConnectTile(left, tile);
        ConnectTile(bottomLeft, tile);
        ConnectTile(bottomRight, tile);
        ConnectTile(right, tile);
        ConnectTile(topRight, tile);
    }

    private void ConnectTile(Vector2Int pos, HexTile tile)
    {
        if(IsPositionInGrid(pos.x, pos.y))
        {
            tile.AdjacentTiles.Add(tileGrid[pos.x, pos.y]);
        }
    }

    private bool IsPositionInGrid(int xPos, int yPos)
    {
        return (xPos >= 0 && xPos < column * 2) && (yPos >= 0 && yPos < row);
    }
}
















