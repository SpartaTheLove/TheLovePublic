using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CreaftingOuputSlot : DragItemSlot
{
    protected Image _icon;
    protected Sprite _baseSprite;

    private void Awake()
    {
        base.Awake();
        _icon = transform.Find("Icon").GetComponent<Image>();
        _baseSprite = _icon.sprite;
    }

    public void Set()
    {
        _icon.sprite = Item.icon;
    }

    public void Clear()
    {
        Item = null;
        _icon.sprite = _baseSprite;
    }
}
