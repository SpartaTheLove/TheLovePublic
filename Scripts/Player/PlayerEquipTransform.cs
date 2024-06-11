using UnityEngine;

public class PlayerEquipTransform : MonoBehaviour
{
    [Header("Equip Transform")]
    public Transform EquipParent;

    [Header("Arrow Transform")]
    public Transform ArrowPos;
    public Transform ArrowEquipParent;

    [Header("Bow Stirng Transform")]
    public Transform bowString;
    public Transform stringInitialPos;
    public Transform stringHandPullPos;
    public Transform stringInitialParent;
}