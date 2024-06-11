using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour
{
    public PlayerInputController inputController;
    public PlayerEquipTransform PlayerEquipTransform;
    public PlayerCondition condition;
    public Inventory Inventory;
    public Building Building;

    private void Awake()
    {
        CharacterManager.Instance.Player = this;
        Inventory = GetComponent<Inventory>();
        inputController = GetComponent<PlayerInputController>();
        condition = GetComponent<PlayerCondition>();
        Building = GetComponent<Building>();
        PlayerEquipTransform = GetComponent<PlayerEquipTransform>();
    }
}