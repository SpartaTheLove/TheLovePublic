using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragItemSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public ItemData Item { get; set; }
    protected GameObject _dragIcon;

    private RectTransform _canvasRectTransform;

    protected void Awake()
    {
        _canvasRectTransform = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        SoundManager.Instance.Play(4, 0.2f);
        //�巡�� ���� ��, �̹��� ������Ʈ�� �ϳ� �����Ѵ�.
        if (Item != null)
        {
            _dragIcon = new GameObject("DragIcon", typeof(Image));
            var image = _dragIcon.GetComponent<Image>();
            image.sprite = Item.icon;
            image.raycastTarget = false;

            Color color = image.color;
            color.a = 0.5f;
            image.color = color;

            _dragIcon.transform.SetParent(_canvasRectTransform, false);
            SetDragIconPosition(eventData.position);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        //������ �̹�����, �巡�� ��ġ�� ���� �̵� ��Ų��.
        if (_dragIcon != null)
        {
            _dragIcon.transform.position = eventData.position;
        }

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //��� ���� ��, ������ �̹����� ���ش�.         
        if (_dragIcon != null)
        {
            Destroy(_dragIcon);
            _dragIcon = null;
        }
    }

    private void SetDragIconPosition(Vector2 position)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvasRectTransform, position, null, out Vector2 localPoint);
        _dragIcon.GetComponent<RectTransform>().anchoredPosition = localPoint;
    }
}
