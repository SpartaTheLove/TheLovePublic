using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Creafting : MonoBehaviour
{
    public CreaftingSlot[] Slots;
    public CreaftingData[] Datas;
    public CreaftingOuputSlot OutputSlot;

    private int _registerCnt;

    private void Start()
    {
        for (int i = 0; i < Slots.Length; i++)
        {
            Slots[i].OnRegister.AddListener(IncreaseRegisterCnt);
            Slots[i].OnCancelRegister.AddListener(ReduceRegisterCnt);
        }
    }

    private void ReduceRegisterCnt()
    {
        _registerCnt--;
    }

    private void IncreaseRegisterCnt()
    {
        _registerCnt++;
        if (_registerCnt == Slots.Length)
        {
            CreaftingItem();
            _registerCnt = 0;
        }
    }

    private void CreaftingItem()
    {
        // ũ�����ÿ� Input���� ���� �����۵� ��ųʸ� ������ ��Ƶ�
        Dictionary<int, int> InputItems = new Dictionary<int, int>();
        foreach (var slot in Slots)
        {
            if (slot.Item != null)
            {
                int itemId = slot.Item.ItemID;
                if (InputItems.ContainsKey(itemId))
                {
                    InputItems[itemId]++;
                }
                else
                {
                    InputItems[itemId] = 1;
                }
            }
        }
        // ���չ��� �´°� �ִ��� ��
        foreach (var data in Datas)
        {
            bool recipeMatch = true;
            foreach (var requirement in data.InputItems)
            {
                if (!InputItems.ContainsKey(requirement.ItemID) || InputItems[requirement.ItemID] < requirement.count)
                {
                    recipeMatch = false;
                    break;
                }
            }

            if (recipeMatch)
            {
                OutputSlot.Item = data.OutData;
                OutputSlot.Set();
                foreach (var slot in Slots)
                {
                    slot.Clear();
                }
                break;
            }
        }
    }
}
