using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class OneQuest : MonoBehaviour
{
    public int QuestNumber;//Номер квеста(по порядоку появления, начиная с 0)
    public string QuestDialogText;//Описание квеста для диалога
    public string QuestGUIText;//Описание квеста для окна квестов
    public int[] WhichObjectsCollect;//Какие типы объектов надо собрать
    public int[] HowManyObjectsCollect;//Как много объектов надо собрать(заполнять СООТВЕТСВЕННО предыдущему массиву)
    public int[] WhichObjectsUpgrade;//Какие типы объектов надо улучшить
    public int[] HowManyObjectsUpgrade;//Сколько объектов нужных типов надо улучшить(заполнять СООТВЕТСВЕННО предыдущему массиву)
    public bool Complete;//Квест выполнен? (!НЕ изменять вручную!)
}
