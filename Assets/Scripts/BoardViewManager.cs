using System.Collections;
using UnityEngine;

public class BoardViewManager : MonoBehaviour
{
    public CellView cellView;

    private int width, height;
    private CellView[,] viewGrid;

    private GridHelper gridHelper;

    public void Init(CellData[,] dataGrid)
    {
        this.width = dataGrid.GetLength(0);
        this.height = dataGrid.GetLength(1);

        viewGrid = new CellView[width, height];

        int offsetX = width / 2;
        int offsetY = height / 2;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                // cellView was never created 
                Vector3 position = new Vector3(x - offsetX, -y + offsetY, 0);
                CellView view = Instantiate<CellView>(cellView, position, Quaternion.identity);
                viewGrid[x, y] = view;
            }
        }

        gridHelper = new GridHelper(dataGrid, viewGrid);
    }

    public IEnumerator UpdateViewFromData()
    {
        CellData[,] gridData = gridHelper.dataGrid;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                {
                    viewGrid[x, y].InitializeSprite(gridData[x, y], gridHelper);
                }
            }
        }

        yield return null;
    }

    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}
}
