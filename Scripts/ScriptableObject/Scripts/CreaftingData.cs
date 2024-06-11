using UnityEngine;

[System.Serializable]
public class ICraftable
{
    public int ItemID;
    public int count;
}

[CreateAssetMenu(fileName = "Creafting", menuName = "New Data/CreaftingData")]
public class CreaftingData : ScriptableObject
{
    [Header("Input Items")]
    public ICraftable[] InputItems;

    [Header("Output Item")]
    public ItemData OutData;
}
