using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDesription : MonoBehaviour
{
    public string DescriptionString;//Текст описания

    private Text DescriptionText;

    void Awake()
    {
        DescriptionText = GetComponentInChildren<Text>();
    }

    void Update()
    {
        DescriptionText.text = @"Description:
" + DescriptionString;
    }
}
