using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeWeaponQuest", menuName = "New Data/QuestData/UpgradeWeaponQuest")]
public class UpgradeWeaponQuest : QuestData
{
    [Header("Achievement Requirements")]
    public List<GameObject> Weapons;

    public override bool CheckComplete()
    {
        // 플레이어 무기 로직 물어보기 -> 인벤에 아이템 두개 존재하는지 아니면 기존활 -> 정화 활로 바뀌는지?
        return true;
    }
}