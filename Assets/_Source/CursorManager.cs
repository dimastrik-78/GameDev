using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorManager : MonoBehaviour
{
    public Text CursorText, UpCursorText;//Текст под курсором, Текст над курсором

    private float NoStaminaTextTimer;
    private TimeManager TM;

    // Start is called before the first frame update
    private void Awake()
    {
        TM = FindObjectOfType<TimeManager>();
    }
    void Start()
    {
        CursorText.text = null;
        UpCursorText.text = null;
    }

    // Update is called once per frame
    void Update()
    {
        NoStaminaTextTimer -= 1 * Time.deltaTime;
        if (NoStaminaTextTimer >= 0) UpCursorText.text = "Ты устал";
        else if (TM.TimeToSleep == true) UpCursorText.text = @"Уже поздно. Пора спать!
Завтра сного дел!";
        else UpCursorText.text = null;
    }
    public void CursorTextOutput(string s, KeyCode kc = KeyCode.None)
    {
        if (kc != KeyCode.None) CursorText.text = s + " - " + kc.ToString();
        else CursorText.text = s;
    }
    public void SleepTimeText()
    {
        UpCursorText.text = @"Уже поздно. Пора спать!
Завтра тебя ждёт много дел!";
    }
    public void NoStaminaText()
    {
        NoStaminaTextTimer = 3;        
    }
}
