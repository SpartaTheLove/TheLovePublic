using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : DragItemSlot, IPointerClickHandler, IDropHandler
{
    public int Index { get; set; }
   
    public int Quantity { get; set; }
    public bool IsEquipped { get; set; }

    public UnityEvent<int, ItemData> OnUseItem = new UnityEvent<int, ItemData>();
    public UnityEvent OnUnEquipItem = new UnityEvent();
    public UnityEvent<ItemData> OnAddSlotItem = new UnityEvent<ItemData>();
    public UnityEvent<ItemData> OnDiscardItem = new UnityEvent<ItemData>();

    private TextMeshProUGUI _quantityText;
    private Outline _outline;
    private Image _icon;
    private Sprite _baseSprite;    

    private float _lastClickTime;
    private const float DoubleClickTime = 0.3f;

    private void Awake()
    {
        base.Awake();
        _outline = gameObject.GetComponent<Outline>();
        _quantityText = transform.Find("Quantity").GetComponent<TextMeshProUGUI>();
        _icon = transform.Find("Icon").GetComponent<Image>();
        
        _baseSprite = _icon.sprite;       
    }

    public void Set()
    {
        if(Quantity <= 0)
        {
            Clear();
            return;
        }
        _icon.sprite = Item.icon;
        _quantityText.text = Quantity > 1 ? Quantity.ToString() : string.Empty;
        _outline.enabled = IsEquipped;
    }

    public void Clear()
    {
        Item = null;
        _icon.sprite = _baseSprite;
        _quantityText.text = string.Empty;
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        SoundManager.Instance.Play(4, 0.2f);
        float currentTimeClick = Time.time;

        // ��Ŭ�� ����
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (Item != null && Item.type != ItemType.House)
            {
                OnDiscardItem.Invoke(Item);
                Clear();
                return;
            }
        }

        // ���� Ŭ�� ����
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (Mathf.Abs(currentTimeClick - _lastClickTime) < DoubleClickTime)
            {
                _lastClickTime = 0;

                if (Item != null)
                {
                    if (IsEquipped)
                    {
                        IsEquipped = false;
                        Set();
                        OnUnEquipItem.Invoke();
                        return;
                    }
                    OnUseItem.Invoke(Index, Item);
                }
            }
            else
            {
                _lastClickTime = currentTimeClick;
            }
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        SoundManager.Instance.Play(4, 0.2f);
        ItemSlot droppedItemSlot = eventData.pointerDrag.GetComponentInParent<ItemSlot>();
        if (droppedItemSlot != null && droppedItemSlot != this)
        {
            ChangeItemSlot(droppedItemSlot);
            return;
        }

        CreaftingSlot droppedCraftingSlot = eventData.pointerDrag.GetComponentInParent<CreaftingSlot>();
        if (droppedCraftingSlot != null && droppedCraftingSlot.Item != null)
        {
            OnAddSlotItem.Invoke(droppedCraftingSlot.Item);
            droppedCraftingSlot.Clear();
            droppedCraftingSlot.CancelRegister();
            return;
        }

        CreaftingOuputSlot droppedOuputSlot = eventData.pointerDrag.GetComponentInParent<CreaftingOuputSlot>();
        if(droppedOuputSlot != null && droppedOuputSlot.Item != null)
        {
            OnAddSlotItem.Invoke(droppedOuputSlot.Item);
            droppedOuputSlot.Clear();
            return;
        }
    }

    public void ChangeItemSlot(ItemSlot droppedSlot)
    {
        // ���� �����̳� ��ӵ� ���� �� �ϳ����� �������� �ִ� ��쿡�� ��ȯ ���
        if (Item != null || droppedSlot.Item != null)
        {
            // ���� �� ������ �����͸� ��ȯ
            ItemData tempItem = droppedSlot.Item;
            int tempQuantity = droppedSlot.Quantity;
            bool tempEquipped = droppedSlot.IsEquipped;

            droppedSlot.Item = Item;
            droppedSlot.Quantity = Quantity;
            droppedSlot.IsEquipped = IsEquipped;
            if (Item != null) droppedSlot.Set();
            else droppedSlot.Clear();

            Item = tempItem;
            Quantity = tempQuantity;
            IsEquipped = tempEquipped;
            Set();
        }
    }
}
