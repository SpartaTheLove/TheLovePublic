using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CreaftingSlot : CreaftingOuputSlot, IDropHandler
{
    public UnityEvent OnRegister = new UnityEvent();
    public UnityEvent OnCancelRegister = new UnityEvent();

    public void CancelRegister()
    {
        OnCancelRegister.Invoke();
    }

    public void OnDrop(PointerEventData eventData)
    {
        ItemSlot droppedSlot = eventData.pointerDrag.GetComponentInParent<ItemSlot>();
        if (droppedSlot != null && droppedSlot != this && droppedSlot.Item != null)
        {
            if (Item != droppedSlot.Item && droppedSlot.Item.type != ItemType.Equipable)
            {
                Item = droppedSlot.Item;
                Set();
                OnRegister.Invoke();
                if (droppedSlot.Quantity > 0)
                {
                    droppedSlot.Quantity--;
                    droppedSlot.Set();
                }
            }           
        }
    }
}