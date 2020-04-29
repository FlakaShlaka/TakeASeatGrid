using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridHelper 
{
    public CellData[,] dataGrid;
    public int aisleIndex;

    public CellView[,] viewGrid;

    public Vector2Int playerPosition = Vector2Int.zero;

    public GridHelper(CellData[,] dataGrid, CellView[,] viewGrid)
    {
        this.dataGrid = dataGrid;
        this.viewGrid = viewGrid;

        for (int y = 0; y < dataGrid.GetLength(1); y++)
        {
            if (dataGrid[0, y].cellType == CellType.aisle)
            {
                aisleIndex = y;
                break;
            }
        }
    }

    public bool InAisle()
    {
        return playerPosition.y == aisleIndex;
    }

    public bool CanMoveUp()
    {
        int x = playerPosition.x;
        int y = playerPosition.y;
        if (y == 0)
        {
            return false;
        }

        if (dataGrid[x, y-1].isSeatTaken)
        {
            return false;
        }

        return true;
    }

    public Vector2 GetMoveUpPosition()
    {
        int x = playerPosition.x;
        int y = playerPosition.y - 1;

        CellView cellView = viewGrid[x, y];
        return GetMoveToPositionOf(cellView);
    }

    public bool CanMoveDown()
    {
        int x = playerPosition.x;
        int y = playerPosition.y;
        if (y == dataGrid.GetLength(1) - 1)
        {
            return false;
        }

        if (dataGrid[x, y + 1].isSeatTaken)
        {
            return false;
        }

        return true;
    }

    public Vector2 GetMoveDownPosition()
    {
        int x = playerPosition.x;
        int y = playerPosition.y + 1;

        CellView cellView = viewGrid[x, y];
        return GetMoveToPositionOf(cellView);
    }

    private Vector2 GetMoveToPositionOf(CellView cellView)
    {
        float posX = cellView.gameObject.GetComponentInParent<Transform>().transform.position.x;
        float posY = cellView.transform.position.y;
        return new Vector2(posX, posY);
    }

    public void PlayerHasTakenASeat()
    {
        int x = playerPosition.x;
        int y = playerPosition.y;

        dataGrid[x, y].isSeatTaken = true;
    }
}
