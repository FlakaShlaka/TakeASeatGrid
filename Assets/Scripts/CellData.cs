
using UnityEngine;

public enum CellType { firstclass, business, thisisus, aisle };

public class CellData 
{
    public static string AISLE_TAG = "aisle";
    
    public CellType cellType = CellType.firstclass;
    public bool isSeatTaken;

    public Vector2Int position;

    public CellData(int x, int y, CellType cellType)
    {
        position = new Vector2Int(x, y);
        this.cellType = cellType;
    }
}
