using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeWeaponQuest", menuName = "New Data/QuestData/UpgradeWeaponQuest")]
public class UpgradeWeaponQuest : QuestData
{
    [Header("Achievement Requirements")]
    public List<GameObject> Weapons;

    public override bool CheckComplete()
    {
        // �÷��̾� ���� ���� ����� -> �κ��� ������ �ΰ� �����ϴ��� �ƴϸ� ����Ȱ -> ��ȭ Ȱ�� �ٲ����?
        return true;
    }
}