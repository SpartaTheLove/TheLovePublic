using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public void OnInteract(); //어떤 효과를 발생시키게 할건지
}

public class Item : MonoBehaviour, IInteractable
{
    public ItemData data;

    public void OnInteract()
    {
        gameObject.SetActive(false);
    }
}
