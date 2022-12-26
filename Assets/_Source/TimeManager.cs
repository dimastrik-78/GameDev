using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public int MaxDayCount;//Максмальное количество дней
    public float MultiplierTime;//Множитель времени, то есть сколько секунд будет идти 1 игровой день (86400 - Одинаково)
    public int DayCount;//Количество прожитых дней (!НЕ изменять вручную!)
    public int CountNewQuestEveryDay;//Количество новых квестов в день
    public Transform ClockImage;//Изображение стрелки часов
    public float StartDayTime;//Реальное время после пробуждения в секундах(осчёт начинается с 0(0 часов/12 часов ночи), заканчивается 43200(12 часов дня); 1 час = 3600)
    public bool TimeToSleep;//пора ли спать? (!НЕ изменять вручную!)

    public float TimeCount;
    private Text DayText;
    private DialogManager DM;
    public int HalfDay;
    private CursorManager CM;
    private AudioManager AM;

    // Start is called before the first frame update
    private void Awake()
    {
        CM = FindObjectOfType<CursorManager>();
        DM = FindObjectOfType<DialogManager>();
        DayText = GetComponentInChildren<Text>();
        AM = FindObjectOfType<AudioManager>();
    }
    void Start()
    {
        DayCount = 0;
        DM.newdialogscount = CountNewQuestEveryDay;
        NewDay();
    }

    // Update is called once per frame
    void Update()
    {
        if ((TimeCount >= 22 * MultiplierTime / 24) || (TimeCount <= 6 * MultiplierTime / 24)) TimeToSleep = true;
        else TimeToSleep = false;

        DayText.text = "День " + DayCount.ToString();

        TimeCount += 1 * Time.deltaTime;
        if (TimeCount >= MultiplierTime)
        {
            TimeCount = 0;
            HalfDay++;
            if (HalfDay >=1)
            {
                DayCount++;
                DM.newdialogscount += CountNewQuestEveryDay;
                HalfDay = 0;
            }
        }

        ClockImage.rotation = Quaternion.Euler(0,0, -(360 * TimeCount / (MultiplierTime/2)));
    }
    public void NewDay()
    {
        AM.PlaySound(1);
        DayCount++;
        HalfDay = 0;
        TimeCount = (StartDayTime * (MultiplierTime / 2)) / 43200;       
    }
}
