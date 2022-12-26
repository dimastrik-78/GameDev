using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaManager : MonoBehaviour
{
    public float MinStamina;//Минимальная Стамина
    public float MaxStamina;//Максимальная Стамина
    public float Stamina;//Количество Стамины в данный момент (!НЕ изменять вручную!)
    public Image BarImage;//Картинка Бара стамины
    public Image SleepBlackPanel;//Панель - имитирующая закрывание глаз(затемнение камеры)
    public float NewDayTimerInterval;//Длительность звука пробуждения
    public bool Sleeping;//Запуск процесса сна
    public GameObject YNbottons;//Пустышка с кнопками(ДА/НЕТ) и текстом "Хотите спать?"

    private bool YesSleep;
    private bool NoSleep;
    private float NewDayTimer;
    private float AlphaTimer;
    private byte ColorAlpha;
    private MoveController MC;
    private CursorManager CM;

    // Start is called before the first frame update
    private void Awake()
    {
        MC = FindObjectOfType<MoveController>();
        CM = FindObjectOfType<CursorManager>();
    }
    void Start()
    {
        NewDayTimerInterval = 1;
        Stamina = MaxStamina;
        NewDayTimer = NewDayTimerInterval;
        YNbottons.SetActive(false);
        YesSleep = false;
        NoSleep = false;
    }

    // Update is called once per frame
    void Update()
    {
        BarImage.fillAmount = Stamina/(MaxStamina-MinStamina);

        if (Sleeping == true)
        {
            MC.CanMove = false;
            Sleep();
        }
    }
    private void Sleep()
    {
        
        if (ColorAlpha < 255 && YesSleep == false && NoSleep == false)
        {
            AlphaTimer -= 300 * Time.deltaTime;
            if(AlphaTimer <= 0)
            {
                ColorAlpha += 1;
                AlphaTimer = 1;

            }
            SleepBlackPanel.color = new Color32(0,0,0,ColorAlpha);
        }
        else if(ColorAlpha <= 255 || (YesSleep == true || NoSleep == true))
        {
            YNbottons.SetActive(true);
            NewDay(YesSleep);
            OldDay(NoSleep);
        }
    }
    public void YesButton()
    {
        YesSleep = true;
    }
    public void NoButton()
    {
        NoSleep = true;
    }
    private void NewDay(bool b)
    {
        if(b == true)
        {
            Stamina = MaxStamina;
            
            YNbottons.SetActive(false);
            NewDayTimer -= 1 * Time.deltaTime;
            if (NewDayTimer <= 0)
            {
                if (ColorAlpha > 0)
                {
                    AlphaTimer -= 300 * Time.deltaTime;
                    if (AlphaTimer <= 0)
                    {
                        ColorAlpha -= 1;
                        AlphaTimer = 1;
                    }
                    SleepBlackPanel.color = new Color32(0, 0, 0, ColorAlpha);
                }
                else
                {                    
                    YesSleep = false;
                    Sleeping = false;
                    NewDayTimer = NewDayTimerInterval;
                    MC.CanMove = true;
                }
            }            
        }
    }
    private void OldDay(bool b)
    {
        if (b == true)
        {
            YNbottons.SetActive(false);
            if (ColorAlpha > 0)
            {
                AlphaTimer -= 300 * Time.deltaTime;
                if (AlphaTimer <= 0)
                {
                    ColorAlpha -= 1;
                    AlphaTimer = 1;
                }
                SleepBlackPanel.color = new Color32(0, 0, 0, ColorAlpha);
            }
            else
            {
                NoSleep = false;
                Sleeping = false;
                NewDayTimer = NewDayTimerInterval;
                MC.CanMove = true;
            }
        }
    }
    public bool HaveNeedStamina(float n)
    {
        bool b = false;
        if (Stamina >= n) b = true;
        return b;
    }
}
