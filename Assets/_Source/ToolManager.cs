using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolManager : MonoBehaviour
{
    public int[] ToolTypes; //Все типы инструментов
    public GameObject[] ToolInHand; //Инструменты в руке(просто визуал по ТЗ) ВАЖНО заполнять СООТВЕТСТВЕННО порядку типов
    public GameObject UpgradeParticles; //Объект с системой частиц(префаб)

    private Cell ToolCell;
    private StaminaManager SM;
    private CursorManager CM;
    private AudioManager AM;

    void Awake()
    {
        ToolCell = FindObjectOfType<InventoryManager>().cells[12];
        SM = FindObjectOfType<StaminaManager>();
        CM = FindObjectOfType<CursorManager>();
        AM = FindObjectOfType<AudioManager>();
    }
    
    public void SetToolInHand()
    {
        for (int i = 0; i < ToolTypes.Length; i++)
        {
            if (ToolCell.ItemType == ToolTypes[i]) ToolInHand[i].SetActive(true);
            else ToolInHand[i].SetActive(false);
        }
    }
    
    public void UpgradeFarm(ObjectToCollect otc, StaminaManager sm)
    {
        if (SM.HaveNeedStamina(otc.UpgradeStaminaRequirement) == true)
        {
            AM.PlaySound(0);
            otc.GettingCount++;
            sm.Stamina -= otc.UpgradeStaminaRequirement;
            otc.CanUpgrade = false;
            GameObject p = Instantiate(UpgradeParticles, otc.transform.position, Quaternion.Euler(0,0,0));
            Destroy(p, 3);
        }           
    }
    
    public bool CanUpgrade(ObjectToCollect otc, int tooltype)
    {
        bool b = false;
        if (otc.TypeToolToUpgrade == tooltype && otc.CanUpgrade == true) b = true;
        return b;
    }
}
