using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIInventory : MonoBehaviour
{
    public ItemSlot[] Slots;

    private void Awake()
    {
        InitSlots();
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        UpdateUI();
    }

    public void InitSlots()
    {
        for (int i = 0; i < Slots.Length; i++)
        {
            Slots[i].Index = i;
            Slots[i].Clear();
            Slots[i].OnAddSlotItem.AddListener(AddSlotItem);
        }
    }

    public void UpdateUI()
    {
        for (int i = 0; i < Slots.Length; i++)
        {
            if (Slots[i].Item != null)
            {
                Slots[i].Set();
            }
            else
            {
                Slots[i].Clear();
            }
        }
    }

    public void AddSlotItem(ItemData data)
    {
        if (data.canStack)
        {
            ItemSlot slot = GetItemStack(data);
            if (slot != null)
            {
                slot.Quantity++;
                UpdateUI();
                return;
            }
        }

        ItemSlot emptySlot = GetEmptySlot();

        if (emptySlot != null)
        {
            emptySlot.Item = data;
            emptySlot.Quantity = 1;
            UpdateUI();
            return;
        }
    }

    ItemSlot GetItemStack(ItemData data)
    {
        for (int i = 0; i < Slots.Length; i++)
        {
            if (Slots[i].Item == data && Slots[i].Quantity < data.maxStackAmount)
            {
                return Slots[i];
            }
        }
        return null;
    }

    ItemSlot GetEmptySlot()
    {
        for (int i = 0; i < Slots.Length; i++)
        {
            if (Slots[i].Item == null)
            {
                return Slots[i];
            }
        }
        return null;
    }

    public void RemoveSlotItem(int index)
    {
        Slots[index].Clear();
    }

    public void ReduceSlotItemQuantity(int index)
    {
        Slots[index].Quantity--;
        Slots[index].Set();
    }

    public int GetSlotItemQuantity(int index)
    {
        return Slots[index].Quantity;
    }

    public void ToggleSlotItemEquipped(int index)
    {
        Slots[index].IsEquipped = !Slots[index].IsEquipped;
        Slots[index].Set();
    }
}
