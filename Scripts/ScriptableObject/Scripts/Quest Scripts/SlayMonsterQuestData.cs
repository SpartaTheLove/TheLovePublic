using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SlayMonsterQuest", menuName = "New Data/QuestData/SlayMonsterQuest")]
public class SlayMonsterQuestData : QuestData
{
    [Header("Achievement Requirements")]
    public List<Enemy> Enemies;
    public List<int> RequiredKillCount;
    public List<int> NowKillCount;

    public override bool CheckComplete()
    {
        for(int i = 0; i<Enemies.Count; i++)
        {
            if (RequiredKillCount[i] >= NowKillCount[i]) return false;
        }
        return true;
    }
}