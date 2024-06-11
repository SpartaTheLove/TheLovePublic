using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public void OnInteract(); //� ȿ���� �߻���Ű�� �Ұ���
}

public class Item : MonoBehaviour, IInteractable
{
    public ItemData data;

    public void OnInteract()
    {
        gameObject.SetActive(false);
    }
}
