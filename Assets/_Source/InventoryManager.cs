using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public Cell[] cells;//Массив для ячеек
    public int cellsCount;//Количество доступных ячеек

    private ItemDesription IDes;
    private ToolManager TM;
    private CharacterOpportunities CO;
    private MoveController MC;

    private void Awake()
    {
        IDes = FindObjectOfType<ItemDesription>();
        IDes.gameObject.SetActive(false);
        TM = FindObjectOfType<ToolManager>();
        MC = FindObjectOfType<MoveController>();
    }
    void Start()
    {
        InventoryReBlocking();
        
    }
    
    public void InvOpenClose()
    {
        if (gameObject.activeSelf == false)
        {
            MC.CanMove = false;
            Time.timeScale = 0;
            gameObject.SetActive(true);
            DescriptionOpenClose();
            InventoryReBlocking();
        }
        else
        {
            MC.CanMove = true;
            Time.timeScale = 1;
            gameObject.SetActive(false);
            DescriptionOpenClose();
            InventoryReBlocking();
        }
    }
    public void InventoryReBlocking()
    {
        for (int i = 0; i < 12; i++)
        {
            if (i + 1 <= cellsCount) cells[i].BlockedCell(false);
            else cells[i].BlockedCell(true);
        }
    }
    public int FindFreeCell()
    {
        int freecell = -1;
        for (int i = 0; i < 12; i++)
        {
            if(cells[i].ItemType == 0)
            {
                freecell = i;
                i = 12;
            }
        }
        return freecell;
    }
    public int FindTypeItemInCell(int needtype)
    {
        int cellNumb = -1;
        for (int i = 0; i < 12; i++)
        {
            if(cells[i].ItemType == needtype)
            {
                cellNumb = i;
                i = 12;
            }
        }
        return cellNumb;
    }
    public void DescriptionOpenClose(string des = null)
    {
        IDes.DescriptionString = des;
        if (IDes.DescriptionString != null) IDes.gameObject.SetActive(true);
        else IDes.gameObject.SetActive(false);
    }
    public void SetToolItem(int cellid)
    {
        if (cells[cellid].isTool == true && cells[12].ItemType == 0)
        {
            cells[12].CellSprite = cells[cellid].CellSprite;
            cells[12].ItemType = cells[cellid].ItemType;
            cells[12].ItemDescriptionString = cells[cellid].ItemDescriptionString;
            RemoveItem(cellid,0,true);
            TM.SetToolInHand();
            InvOpenClose();
        }       
    }
    public void RemoveToolItem()
    {   
        int i = FindFreeCell();
        if (i != -1 && cells[12].ItemType != 0)
        {
            cells[i].ItemType = cells[12].ItemType;
            cells[i].CellSprite = cells[12].CellSprite;
            cells[i].ItemCount = 1;
            cells[i].ItemDescriptionString = cells[12].ItemDescriptionString;
            cells[i].isTool = true;           
            RemoveItem(12, 0, true);
            TM.SetToolInHand();
            InvOpenClose();
        }
    }
    public void RemoveItem(int cellNumb, int itemcount = 0, bool tools = false)
    {
        if (cells[cellNumb].ItemCount > itemcount && itemcount != 0)
        {

            cells[cellNumb].ItemCount -= itemcount;
        }
        else
        {
            if (cells[cellNumb].isTool == false)
            {
                cells[cellNumb].ItemType = 0;
                cells[cellNumb].ItemCount = 0;
                cells[cellNumb].CellSprite = cells[cellNumb].NullSprite;
                cells[cellNumb].ItemDescriptionString = null;
                cells[cellNumb].isTool = false;
            }
            else
            {
                if (tools == true)
                {
                    cells[cellNumb].ItemType = 0;
                    cells[cellNumb].ItemCount = 0;
                    cells[cellNumb].CellSprite = cells[cellNumb].NullSprite;
                    cells[cellNumb].ItemDescriptionString = null;
                    cells[cellNumb].isTool = false;
                }
            }
        }
    }
    public bool IsInvEmpty(bool t = false)
    {
        bool b = true;
        for (int i = 0; i < cells.Length; i++)
        {
            if (cells[i].ItemType != 0 && cells[i].isTool == t) b = false;
        }
        return b;
    }
}
