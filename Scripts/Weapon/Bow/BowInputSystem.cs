using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
public class BowInputSystem : MonoBehaviour
{
    PlayerAnimController _playerAnimController;
    CameraController _cameraController;

    [System.Serializable]
    public class InputSettings
    {
        public string aim = "Fire2";
        public string fire = "Fire2";
    }
    [SerializeField]
    public InputSettings input;

    [Header("Aiming Settings")]
    Ray ray;
    RaycastHit hit;
    public LayerMask aimLayers;

    [Header("Head Rotation Settings")]
    public float lookAtPoint = 2.8f;
    
    public Bow bowScript;
    public bool testAim;
    public bool isAiming;
    public bool isAimingDown;
     
    bool hitDetected;
    public GameObject arrow;
    
    Animator playerAnim;
    private void Awake()
    {
        _playerAnimController = GetComponent<PlayerAnimController>();
        _cameraController = GetComponentInChildren<CameraController>();
        playerAnim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        arrow.SetActive(isAiming);

        UpdateAim();

        if (CheckPlayerHaveBow())
        {
            if (isAiming)
            {
                Pull();
                Aim();

                if (isAimingDown) SoundManager.Instance.Play(0, 0.2f);
                _playerAnimController.PullString(Input.GetButton(input.fire));
            }
            else if (Input.GetButtonUp(input.aim))
            {
                _playerAnimController.FireArrow();
                if (hitDetected)
                {
                    bowScript.Fire(hit.point);
                }
                else
                {
                    bowScript.Fire(ray.GetPoint(300f));
                }
            }
            else
            {
                bowScript.RemoveCrosshair();
                Release();
            }
        }
    }

    bool CheckPlayerHaveBow()
    {
        GameObject obj = CharacterManager.Instance.Player.Inventory._curEquip;
        if (obj != null)
        {
            if(obj.GetComponent<Item>().data.ItemID ==4 )
            {
                return true;
            }
        }
        return false;
    }


    public void UpdateAim()
    {
        isAiming = Input.GetButton(input.aim);
        isAimingDown = Input.GetButtonDown(input.aim);
        _playerAnimController.Aim(isAiming);
    }
    private void Aim()
    {
        //Vector3 mousePosition = Input.mousePosition;
        //Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        
        Vector3 camPosition = Camera.main.transform.position;
        Vector3 dir = _cameraController.mainCam.transform.forward;

        ray = new Ray(camPosition, dir);

        if (Physics.Raycast(ray, out hit, 500f, aimLayers))
        {
            hitDetected = true;
            Debug.DrawLine(ray.origin, hit.point, Color.red);
            bowScript.ShowCrosshair(hit.point);
        }
        else
        {
            hitDetected = false;
            bowScript.RemoveCrosshair();
        }
    }

    public void Pull()
    {
        bowScript.PullString();
    }
    public void EnableArrow()
    {
        bowScript.PickArrow();
    }
    public void DisableArrow()
    {
        bowScript.DisableArrow();
    }
    public void Release()
    {
        bowScript.ReleaseString();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (isAiming)
        {
            playerAnim.SetLookAtWeight(1f);
            playerAnim.SetLookAtPosition(ray.GetPoint(lookAtPoint));
        }
        else
        {
            playerAnim.SetLookAtWeight(0);
        }
    }
}
