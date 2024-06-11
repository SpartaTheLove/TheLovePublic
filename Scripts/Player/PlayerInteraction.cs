using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{   
    [SerializeField] float MaxCheckDistance;
    [SerializeField] LayerMask Layer;
    [SerializeField] float CheckRate = 0.05f;

    private float _lastCheckTime;
    private PlayerController _playerController;
    private Camera _camera;

    [HideInInspector]
    public GameObject CurInteractGameObject;
    private IInteractable _curInteractable;
    private Inventory _inventory;

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        _inventory = GetComponent<Inventory>();
    }
    private void Start()
    {
        _camera = Camera.main;
        _playerController.OnInteractEvent += Interaction;
    }
    private void Update()
    {
        if(Time.time - _lastCheckTime > CheckRate)
        {
            _lastCheckTime = Time.time;
            OnHit();
        }
    }

    private void OnHit()
    {
        Ray ray = _camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, MaxCheckDistance, Layer))
        {
            if (hit.collider.gameObject != CurInteractGameObject)
            {
                CurInteractGameObject = hit.collider.gameObject;
                _curInteractable = hit.collider.GetComponent<IInteractable>();
            }
        }
        else
        {
            CurInteractGameObject = null;
            _curInteractable = null;
        }
    }

    public void Interaction()
    {
        if (_curInteractable != null)
        {
            // 인벤토리에 아이템 넣기
            _inventory.AddItem(CurInteractGameObject.GetComponent<Item>().data);
            _curInteractable.OnInteract();
        }
    }
}
