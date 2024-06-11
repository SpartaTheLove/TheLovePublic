using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("Inventory")]
    [SerializeField] GameObject Axe;
    [SerializeField] GameObject HouseBg;
    [SerializeField] Transform DropPosition;
    public UIInventory UIInventory;
    public GameObject _curEquip;

    private List<ItemData> _itemDatas = new List<ItemData>();   
    private PlayerCondition _playerCondition;
    public BowInputSystem BowInputSystem;

    private void Awake()
    {
        _playerCondition = GetComponent<PlayerCondition>();
    }

    private void Start()
    {
        _curEquip = null;
        foreach (var itemSlot in UIInventory.Slots)
        {
            itemSlot.OnUseItem.AddListener(UseItem);
            itemSlot.OnUnEquipItem.AddListener(UnEquipItem);
            itemSlot.OnDiscardItem.AddListener(ThrowItem);
        }
        AddItem(Axe.GetComponent<Item>().data);
    }

    public void AddItem(ItemData data)
    {
        if (!_itemDatas.Contains(data))
        {
            _itemDatas.Add(data);
            UIInventory.AddSlotItem(data);
        }
        else
        {
            UIInventory.AddSlotItem(data);
        }
    }


    public void ThrowItem(ItemData data)
    {
        Instantiate(data.dropPrefab, DropPosition.position, Quaternion.Euler(Vector3.one * Random.value * 360));
    }

    public void UseItem(int index, ItemData data)
    {
        if (data == null) return;
        switch (data.type)
        {
            case ItemType.Equipable:
                UseEquipable(index, data);
                break;
            case ItemType.Consumable:
                UseConsumable(index, data);
                break;
            case ItemType.House:
                ButtonManager.Instance.OnClickOpen(HouseBg);
                CharacterManager.Instance.Player.GetComponent<Building>().HouseData = data;
                break;
        }
    }

    public void UseEquipable(int index, ItemData data)
    {
        if (data == null) return;
        if (_curEquip != null)
        {
            Destroy(_curEquip.gameObject);
            _curEquip = null;
        }
        _curEquip = Instantiate(data.equipPrefab, CharacterManager.Instance.Player.PlayerEquipTransform.EquipParent);
        _curEquip.SetActive(true);
        UIInventory.ToggleSlotItemEquipped(index);
        if (data.ItemID == 4)
        {
            BowInputSystem.enabled = true;
            BowInputSystem.bowScript = _curEquip.gameObject.GetComponent<Bow>();
        }

        QuestManager.Instance.CallCheckEquipQuest(_curEquip);
    }

    public void UseConsumable(int index, ItemData data)
    {
        if (data == null) return;
        for (int i = 0; i < data.itemDataConsumables.Length; i++)
        {
            switch (data.itemDataConsumables[i].type)
            {
                case ConsumableType.Health:
                    _playerCondition.Heal(data.itemDataConsumables[i].value); break;
                case ConsumableType.Hunger:
                    _playerCondition.Eat(data.itemDataConsumables[i].value); break;
            }
        }

        UIInventory.ReduceSlotItemQuantity(index);
        if (UIInventory.GetSlotItemQuantity(index) <= 0)
        {
            _itemDatas.Remove(data);
            UIInventory.RemoveSlotItem(index);
        }
    }

    public void UnEquipItem()
    {
        if (_curEquip != null)
        {
            Destroy(_curEquip.gameObject);
            _curEquip = null;
        }
    }
}
