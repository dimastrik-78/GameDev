using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectToCollect : MonoBehaviour
{
    public Sprite ObjectImage;//Изображение предмеета(то как он будет выглядеть в инвентаре)
    public int ObjectID;//Тип объекта
    public string ObjectDescription;//Описание предмета(будет также отображаться в инвентаре)
    
    public bool CanStack;//Может ли стакаться?
    public int CollectStaminaRequirement;//Количество Стамины для сбора
    public int GettingCount;//Количество получаемого пердмета при сборе

    public bool CanUpgrade;//Можно ли улучшить?
    public bool isTool;//Предмет - инструмент?
    public int UpgradeStaminaRequirement;//Количество Стамины для улучшения
    public int TypeToolToUpgrade;//Тип инструмента для улучшения
}
