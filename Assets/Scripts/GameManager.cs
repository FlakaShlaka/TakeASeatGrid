using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public BoardViewManager boardViewManager;

    public int width = 11;
    public int height = 5;

    public int numTopRows = 2;

    public int numFirstClass = 3;
    public int numBusiness = 5;

    public CellData[,] dataGrid;


    void Start()
    {
        CreateBoardData();
        boardViewManager.Init(dataGrid);

        StartCoroutine(boardViewManager.UpdateViewFromData());
    }

    private void CreateBoardData()
    {
        // can be called multiple times (e.g. on game restart)
        dataGrid = new CellData[width, height];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                CellType cellType;
                if (x < numFirstClass)
                {
                    cellType = CellType.firstclass;
                }
                else if (x < numFirstClass + numBusiness)
                {
                    cellType = CellType.business;
                }
                else
                {
                    cellType = CellType.thisisus;
                }

                // each column consists of seats + one aisle + seats
                if (y == numTopRows)
                {
                    cellType = CellType.aisle;
                }

                dataGrid[x, y] = new CellData(x, y, cellType);
            }
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
