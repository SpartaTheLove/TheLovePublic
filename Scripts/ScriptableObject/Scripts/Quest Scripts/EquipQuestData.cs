using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestData", menuName = "New Data/QuestData/EquipQuest")]
public class EquipQuestData : QuestData
{
    [Header("Achievement Requirements")]
    public GameObject Equipment;

    // 두 가지 이상 장착 시 사용..
    public override bool CheckComplete()
    {   
        return true;
    }
}
