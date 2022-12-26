using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMagaer : MonoBehaviour
{
    public KeyCode Inventory, PauseMenu, CollectObject, UpgradeObject, SpeakToQuest, Sleep;//Бинд клавиш

    private PauseMenu PM;
    private InventoryManager IM;

    void Awake()
    {
        IM = FindObjectOfType<InventoryManager>();
        PM = FindObjectOfType<PauseMenu>();
    }
    void Start()
    {
        IM.gameObject.SetActive(false);
        PM.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(Inventory))
        {
            IM.InvOpenClose();           
        }
        if (Input.GetKeyDown(PauseMenu))
        {
            PM.PauseOpenClose();
        }
    }
}
