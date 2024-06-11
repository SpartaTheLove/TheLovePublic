using UnityEngine;

public enum QuestType
{
    EquipQuest,
    SlayMonsterQuest,
    UpgradeWeaponQuest
}

public abstract  class QuestData : ScriptableObject
{
    [Header("Info")]
    public int QuestID;
    public QuestType QuestType;
    public string QuestName;
    public string QuestDescription;
    public bool IsProcessing;
    public bool IsClear;
    public bool GetRewards;

    [Header("DropItem")]
    public GameObject[] Rewards;

    public abstract bool CheckComplete();
}
