using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorManager : MonoBehaviour
{
    public Text CursorText, UpCursorText;//Текст под курсором, Текст над курсором

    private float NoStaminaTextTimer;
    private TimeManager TM;

    private void Awake()
    {
        TM = FindObjectOfType<TimeManager>();
    }
    void Start()
    {
        CursorText.text = null;
        UpCursorText.text = null;
    }

    void Update()
    {
        NoStaminaTextTimer -= 1 * Time.deltaTime;
        if (NoStaminaTextTimer >= 0) UpCursorText.text = "Are you tired";
        else if (TM.TimeToSleep == true) UpCursorText.text = @"Too late. Time to sleep!
Tomorrow is busy!";
        else UpCursorText.text = null;
    }
    public void CursorTextOutput(string s, KeyCode kc = KeyCode.None)
    {
        if (kc != KeyCode.None) CursorText.text = s + " - " + kc.ToString();
        else CursorText.text = s;
    }
    public void SleepTimeText()
    {
        UpCursorText.text = @"Too late. Time to sleep!
Tomorrow you have a lot to do!";
    }
    public void NoStaminaText()
    {
        NoStaminaTextTimer = 3;        
    }
}
