using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestsManager : MonoBehaviour
{
    public List<OneQuest> PriorityQuest;//Доступные квесты в приоритетном порядке (!НЕ изменять вручную!)
    public GameObject QuestEmpty;//Пустой объект со всеми квестами в игре
    public List<OneQuest> CurrentQuests;//Все доступные квесты в порядке появления по дням (!НЕ изменять вручную!)

    private OneQuest[] AllQuests;//Все квесты в порядке появления по дням    
    private int CompleteQuestsCount;
    private int AvalibleQuestCount;//Количество всех появившихся квестов(выполненных тоже)
    private Text[] QuestText;
    private InventoryManager IM;
    

    // Start is called before the first frame update
    private void Awake()
    {
        AddAllQuests();
        IM = FindObjectOfType<InventoryManager>();
    }
    void Start()
    {
        QuestText = GetComponentsInChildren<Text>();
        
    }

    // Update is called once per frame
    void Update()
    {   
        CompletingQuestType1();
        AddQuestToGUI();
    }
    public void AddNewQuest()
    {
        if (AvalibleQuestCount < AllQuests.Length)
        {
            CurrentQuests.Add(AllQuests[AvalibleQuestCount]);
            AvalibleQuestCount++;
            FillPreorityQuests();
        }
    }
    private void AddQuestToGUI()
    {
        string s = null;
        for (int i = 0; i < PriorityQuest.Count; i++)
        {
            s += i+1 + ")" + PriorityQuest[i].QuestGUIText + @"
";
        }
        QuestText[1].text = s; 
    }
    public void FillPreorityQuests()
    {
        PriorityQuest = new List<OneQuest>();

        for (int i = 0; i < CurrentQuests.Count; i++)
        {
            if(CurrentQuests[i].WhichObjectsCollect.Length != 0)
            {
                PriorityQuest.Add(CurrentQuests[i]);
            }
        }
        for (int i = 0; i < CurrentQuests.Count; i++)
        {
            if (CurrentQuests[i].WhichObjectsUpgrade.Length != 0)
            {
                PriorityQuest.Add(CurrentQuests[i]);
            }
        }
    }
    private void AddAllQuests()
    {
        AllQuests = new OneQuest[QuestEmpty.GetComponents<OneQuest>().Length];
        for (int i = 0; i < AllQuests.Length; i++)
        {
            foreach (OneQuest oq in QuestEmpty.GetComponents<OneQuest>())
            {
                if (i == oq.QuestNumber) AllQuests[i] = oq;
            }
        }
    }
    private void CompletingQuestType1()
    {
        for (int i = 0; i < PriorityQuest.Count; i++)
        {
            if (PriorityQuest[i].WhichObjectsCollect.Length != 0 && PriorityQuest[i].Complete == false)
            {
                int c = 0;
                for (int i2 = 0; i2 < PriorityQuest[i].WhichObjectsCollect.Length; i2++)
                {
                    if (IM.FindTypeItemInCell(PriorityQuest[i].WhichObjectsCollect[i2]) != -1)
                    {
                        if (IM.cells[IM.FindTypeItemInCell(PriorityQuest[i].WhichObjectsCollect[i2])].ItemCount >= PriorityQuest[i].HowManyObjectsCollect[i2]) c++;
                    }
                }
                if (c == PriorityQuest[i].HowManyObjectsCollect.Length) 
                    PriorityQuest[i].Complete = true;
            }
        }
    }
    public void GiveAwayItemsforQuestType1()
    {
        for (int i = 0; i < PriorityQuest.Count; i++)
        {
            if (PriorityQuest[i].WhichObjectsCollect.Length != 0 && PriorityQuest[i].Complete == false)
            {
                for (int i2 = 0; i2 < PriorityQuest[i].WhichObjectsCollect.Length; i2++)
                {
                    if (IM.FindTypeItemInCell(PriorityQuest[i].WhichObjectsCollect[i2]) != -1)
                    {
                        int c = IM.cells[IM.FindTypeItemInCell(PriorityQuest[i].WhichObjectsCollect[i2])].ItemCount;
                        PriorityQuest[i].HowManyObjectsCollect[i2] -= c;
                        IM.RemoveItem(IM.FindTypeItemInCell(PriorityQuest[i].WhichObjectsCollect[i2]), c);
                    }
                }
            }
        }
        if (PriorityQuest.Count == 0)
            for (int i = 0; i < IM.cells.Length; i++)
            {
                if (IM.cells[i].ItemType != 0) IM.RemoveItem(i);
            }
    }
    public void CompletingQuestType2(ObjectToCollect otc)
    {     
        for (int i = 0; i < PriorityQuest.Count; i++)
        {
            if (PriorityQuest[i].WhichObjectsUpgrade.Length != 0)
            {
                int c = 0;
                for (int i2 = 0; i2 < PriorityQuest[i].WhichObjectsUpgrade.Length; i2++)
                {
                    if (otc.ObjectID == PriorityQuest[i].WhichObjectsUpgrade[i2]) PriorityQuest[i].HowManyObjectsUpgrade[i2]--;
                    if (PriorityQuest[i].HowManyObjectsUpgrade[i2] == 0) c++;
                }
                if (c == PriorityQuest[i].WhichObjectsUpgrade.Length) PriorityQuest[i].Complete = true;
            }            
        }
    }
    public bool HaveCompletedQuestst()
    {
        bool b = false;
        for (int i = 0; i < PriorityQuest.Count; i++)
        {
            if(PriorityQuest[i].Complete == true)
            {
                b = true;
            }
        }
        return b;
    }
    public void DeleteCompletedQuests()
    {
        for (int i = 0; i < PriorityQuest.Count; i++)
        {
            if(PriorityQuest[i].Complete == true)
            {
                if(PriorityQuest[i].WhichObjectsCollect.Length != 0)
                {
                    for (int i2 = 0; i2 < PriorityQuest[i].WhichObjectsCollect.Length; i2++)
                    {                        
                        IM.RemoveItem(IM.FindTypeItemInCell(PriorityQuest[i].WhichObjectsCollect[i2]), PriorityQuest[i].HowManyObjectsCollect[i2]);
                    }
                }
                CurrentQuests.Remove(PriorityQuest[i]);
                FillPreorityQuests();
                AddQuestToGUI();
            }
        }
    }
}
