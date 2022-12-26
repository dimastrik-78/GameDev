using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Cell : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Sprite CellSprite, NullSprite; //Спрайт предмета в ячейке; Спрайт пустого слота (!НЕ изменять вручную!)
    public int ItemType; //Тип предмета в ячейке (!НЕ изменять вручную!)
    public int ItemCount; //Количество предмета в ячейке (!НЕ изменять вручную!)
    public bool isTool; //Предмет в ячейке - инструмент? (!НЕ изменять вручную!)
    public string ItemDescriptionString; //Описание предмета в ячейке (!НЕ изменять вручную!)
    public Image CellImage; //Изображение Предмета(то, куда будет помещаться Спрайт предмета)

    private Button CellButton;
    private Text CountText;
    private Color32 colorOn;
    private Color32 colorDis;
    private InventoryManager IM;

    void Awake()
    {
        CellButton = gameObject.GetComponent<Button>();
        CountText = gameObject.GetComponentInChildren<Text>();
        IM = FindObjectOfType<InventoryManager>();
        colorOn = new Color32(168, 168, 168,255);
        colorDis = new Color32(200, 0, 0,255);
    }

    void Update()
    {   
        CellImage.sprite = CellSprite;
        if (ItemType == 0) CellSprite = NullSprite;
        if(CountText != null) CountText.text = ItemCount.ToString();
    }
    public void BlockedCell(bool blocked)
    {
        CellButton.interactable = !blocked;
        CellImage.enabled = !blocked;
        if (blocked == true) CellButton.targetGraphic.color = colorDis;
        else CellButton.targetGraphic.color = colorOn;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(ItemType != 0) IM.DescriptionOpenClose(ItemDescriptionString);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        IM.DescriptionOpenClose(null);
    }
}
