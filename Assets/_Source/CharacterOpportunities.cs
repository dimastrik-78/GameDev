using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterOpportunities : MonoBehaviour
{
    public float InteractionDistance;//Дистанция для взаимодействия с предметами
    public LayerMask InteractableObjectsMask;//Маска для отсеивания препядствий(для луча)

    private InputMagaer IP;
    private InventoryManager IM;
    private RaycastHit hit;
    private CursorManager CM;
    private StaminaManager SM;
    private ToolManager TM;
    private DialogManager DM;
    private QuestsManager QM;

    void Awake()
    {
        IP = FindObjectOfType<InputMagaer>();
        IM = FindObjectOfType<InventoryManager>();
        CM = FindObjectOfType<CursorManager>();
        SM = FindObjectOfType<StaminaManager>();
        TM = FindObjectOfType<ToolManager>();
        DM = FindObjectOfType<DialogManager>();
        QM = FindObjectOfType<QuestsManager>();
    }

    void Update()
    {
        SingleExceptions();
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, InteractionDistance, InteractableObjectsMask))
        {
            if (hit.collider.CompareTag("ObjectToCollect"))
            {
                ObjectToCollect otc = hit.collider.GetComponent<ObjectToCollect>();
                if (TM.CanUpgrade(otc, IM.cells[12].ItemType) == false)
                {
                    CM.CursorTextOutput("Collect", IP.CollectObject);
                    if (Input.GetKeyDown(IP.CollectObject)) CollectObject(otc, CanCollect(otc));
                }
                else
                {
                    CM.CursorTextOutput("Improve", IP.UpgradeObject);
                    if (Input.GetKeyDown(IP.UpgradeObject))
                    {
                        TM.UpgradeFarm(otc, SM);
                        QM.CompletingQuestType2(otc);
                    }
                }
            }
            if (hit.collider.CompareTag("Grandma"))
            {
                CM.CursorTextOutput("talk", IP.SpeakToQuest);
                if (Input.GetKeyDown(IP.SpeakToQuest))
                {
                    if (DM.gameObject.activeSelf == false)
                    {

                        if (QM.HaveCompletedQuestst() == true)
                        {
                            QM.DeleteCompletedQuests();
                            DM.OpenCompleteQuestWindow();
                        }
                        else
                        {
                            DM.OpenDialogWindow();
                        }
                    }
                    else DM.CloseDialogWindow();
                }
            }
            if (hit.collider.CompareTag("Bed"))
            {
                CM.CursorTextOutput("Sleep", IP.Sleep);
                if (IM.IsInvEmpty() == true)
                    if (Input.GetKeyDown(IP.Sleep)) SM.Sleeping = true;
            }
        }
    }
    private int CanCollect(ObjectToCollect otc)
    {
        int i = -1;
        if (otc.CanStack == true)
            i = IM.FindTypeItemInCell(otc.ObjectID);
        if (i == -1) i = IM.FindFreeCell();
        if (SM.HaveNeedStamina(otc.CollectStaminaRequirement) == false)
        {
            CM.NoStaminaText();
            i = -1;
        }
        return i;
    }
    public void CollectObject(ObjectToCollect otc, int cellNumb)
    {
        if (cellNumb != -1)
        {
            IM.cells[cellNumb].ItemType = otc.ObjectID;
            IM.cells[cellNumb].ItemCount += otc.GettingCount;
            IM.cells[cellNumb].CellSprite = otc.ObjectImage;
            IM.cells[cellNumb].ItemDescriptionString = otc.ObjectDescription;
            IM.cells[cellNumb].isTool = otc.isTool;
            SM.Stamina -= otc.CollectStaminaRequirement;
            Destroy(otc.gameObject);
        }
    }
    private void SingleExceptions()
    {
        CM.CursorTextOutput(null);
        if (hit.collider == null || !hit.collider.CompareTag("Grandma"))
        {
            DM.CloseDialogWindow();
        }
    }
}
