using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellView : MonoBehaviour
{

    public List<Sprite> spriteList = new List<Sprite>();

    public CellData cellData;

    public void InitializeSprite(CellData cellData, GridHelper dataGridHelper)
    {
        this.cellData = cellData;

        GetComponent<MoveController>().gridHelper = dataGridHelper;

        int spriteIndex = 0;
        switch (cellData.cellType)
        {
            case CellType.firstclass:
                spriteIndex = 0;
                break;

            case CellType.business:
                spriteIndex = 1;
                break;

            case CellType.thisisus:
                spriteIndex = 2;
                break;

            case CellType.aisle:
                spriteIndex = 3;
                this.tag = "aisle";
                break;
        }

        Sprite newSprite = spriteList[spriteIndex];

        SpriteRenderer spriteRendered = gameObject.GetComponent<SpriteRenderer>();
        spriteRendered.sprite = newSprite;
    }
}
