using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public float NextTextTimerInterval;//Интервал между разными заданиями бабушки(в окне диалога)
    public int newdialogscount;//количество новых квестов запускать при следующем разговоре (!НЕ изменять вручную!)
    public float DialogTime;//Время, через которое закроется диалоговое окно, после озвучивания всех квестов
    public string CompleteQuestText;//То, что говорит бабушка после успешно сданного(ных) квеста(тов)
    public Text DialogText;//Компонент-текст, куда будет выводится слова бабушки

    private int dialogscount;    
    private QuestsManager QM;
    private float NextTextTimer;
    private float DialogCloseTime;
    private float nexttimerspeed;

    private void Awake()
    {
        QM = FindObjectOfType<QuestsManager>();
        
    }
    void Start()
    {
        CloseDialogWindow();
    }

    void Update()
    {
        NextTextTimer -= nexttimerspeed * Time.deltaTime;
        if (NextTextTimer <= 0)
        {           
            if (dialogscount < QM.PriorityQuest.Count)
            {
                OldDialogs(dialogscount);
                NextTextTimer = NextTextTimerInterval;
                dialogscount++;
            }
            else if (newdialogscount != 0)
            {
                NewDialog(dialogscount);
                newdialogscount--;
            }
        }

        DialogCloseTime -= 1 * Time.deltaTime;
        if (DialogCloseTime <= 0) CloseDialogWindow();
    }
    public void OpenDialogWindow()
    {
        QM.GiveAwayItemsforQuestType1();
        nexttimerspeed = 1;
        gameObject.SetActive(true);
        DialogCloseTime = NextTextTimerInterval * (QM.PriorityQuest.Count-1) + DialogTime;
        NextTextTimer = 0;
        dialogscount = 0;
    }
    public void CloseDialogWindow()
    {
        gameObject.SetActive(false);
    }
    private void NewDialog(int odc)
    {       
        QM.AddNewQuest();
        DialogText.text = QM.PriorityQuest[odc].QuestDialogText;
    }
    private void OldDialogs(int odc)
    {
        QM.FillPreorityQuests();
        DialogText.text = QM.PriorityQuest[odc].QuestDialogText;
    }
    public void OpenCompleteQuestWindow()
    {
        gameObject.SetActive(true);
        NextTextTimer = 1;
        nexttimerspeed = 0;
        DialogText.text = CompleteQuestText;
        DialogCloseTime = DialogTime;
    }
}
