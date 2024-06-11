using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestData", menuName = "New Data/QuestData/EquipQuest")]
public class EquipQuestData : QuestData
{
    [Header("Achievement Requirements")]
    public GameObject Equipment;

    // �� ���� �̻� ���� �� ���..
    public override bool CheckComplete()
    {   
        return true;
    }
}
