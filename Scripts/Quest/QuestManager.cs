using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : Singleton<QuestManager>
{
    public event Action<Enemy> OnCheckSlayMonsterQuest;
    public event Action<GameObject> OnCheckEquipQuest;
    public event Action<Item> OnCheckUpgradeWeaponQuest;

    public List<QuestData> QuestData = new List<QuestData>();

    protected override void Awake()
    {
        OnCheckSlayMonsterQuest += AddSlayedMonster;
        OnCheckEquipQuest += CheckEquipment;
        OnCheckUpgradeWeaponQuest += CheckWeaponUpgrade;
    }

    public void CallCheckSlayMonsterQuest(Enemy enemy)
    {
        OnCheckSlayMonsterQuest?.Invoke(enemy);
    }

    public void CallCheckEquipQuest(GameObject equip)
    {
        OnCheckEquipQuest?.Invoke(equip);
    }

    public void CallCheckUpgradeWeaponQuest(Item item)
    {
        OnCheckUpgradeWeaponQuest?.Invoke(item);
    }


    void AddSlayedMonster(Enemy enemy)
    {
        foreach (QuestData questData in QuestData)
        {
            if (questData.QuestType == QuestType.SlayMonsterQuest && questData.IsProcessing && !questData.IsClear)
            {
                SlayMonsterQuestData data = questData as SlayMonsterQuestData;
                for (int i = 0; i < data.Enemies.Count; i++)
                {
                    if(enemy.GetComponent<Enemy>().Data.ID == data.Enemies[i].GetComponent<Enemy>().Data.ID)
                    {
                        data.NowKillCount[i] += 1;
                    }
                }
                if (data.CheckComplete())
                {
                    ClearQuest(data);
                    // TODO :: QUEST 완료창 띄우기 & 보상 인벤에 넣기
                }
            }
        }
    }

    void CheckEquipment(GameObject equip)
    {
        foreach(QuestData questData in QuestData)
        {
            if (questData.QuestType == QuestType.EquipQuest && questData.IsProcessing && !questData.IsClear)
            {
                EquipQuestData data = questData as EquipQuestData;

                if(data.Equipment.GetComponent<Item>().data.ItemID == equip.GetComponent<Item>().data.ItemID)
                {
                    ClearQuest(data);
                    // TODO :: QUEST 완료창 띄우기 & 보상 인벤에 넣기
                }
            }
        }
    }

    void CheckWeaponUpgrade(Item item)
    {
        foreach (QuestData questData in QuestData)
        {
            if (questData.QuestType == QuestType.UpgradeWeaponQuest && questData.IsProcessing && !questData.IsClear)
            {
                UpgradeWeaponQuest data = questData as UpgradeWeaponQuest;
                for (int i = 0; i < data.Weapons.Count; i++)
                {

                }
                if (data.CheckComplete())
                {
                    data.IsClear = true;
                    // TODO :: QUEST 완료창 띄우기 & 보상 인벤에 넣기
                }
            }
        }
    }

    public void AcceptQuest(QuestData questData)
    {
        questData.IsProcessing = true;
    }

    public void ClearQuest(QuestData questData)
    {
        questData.IsClear = true;
    }

     public void GetRewards(QuestData questData)
    {
        questData.GetRewards = true;
        foreach(GameObject obj in questData.Rewards)
        {
            CharacterManager.Instance.Player.Inventory.AddItem(obj.GetComponent<Item>().data);
        }
    }
}