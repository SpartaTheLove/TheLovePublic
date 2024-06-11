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
        //드래그 시작 시, 이미지 오브젝트를 하나 생성한다.
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
        //생성한 이미지를, 드래그 위치에 맞춰 이동 시킨다.
        if (_dragIcon != null)
        {
            _dragIcon.transform.position = eventData.position;
        }

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //드랍 했을 때, 생성한 이미지를 없앤다.         
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
