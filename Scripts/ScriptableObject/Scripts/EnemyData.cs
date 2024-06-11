using UnityEngine;

[CreateAssetMenu(fileName ="EnemyData", menuName ="New Data/EnemyData")]
public class EnemyData : ScriptableObject
{
    [Header("Info")]
    public int ID;
    public string Name; 
    public float WalkSpeed;
    public float RunSpeed;

    [Header("Attack")]
    public float AttackPower;

    [Header("DropItem")]
    public GameObject[] DropItem;

}
