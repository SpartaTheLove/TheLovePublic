using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : PlayerController
{
    public LayerMask groundLayerMask;

    public bool isRun = false;
    public bool isAttacking = false;

    public void OnMove(InputValue value)
    {
        Vector2 moveInput = value.Get<Vector2>().normalized;
        CallMoveEvent(moveInput);

        if (moveInput != Vector2.zero)
        {
            if(isRun) SoundManager.Instance.Play(3, 0.2f, SoundType.Effect, true);
            else SoundManager.Instance.Play(3, 0.2f); 
        }
        else
        {
            SoundManager.Instance.Stop();
        }
    }

    public void OnJump(InputValue value)
    {
        if (value.isPressed && IsGrounded())
        {
            CallJumpEvent();
        }
    }

    bool IsGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.1f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.1f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.1f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) + (transform.up * 0.1f), Vector3.down)
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 1f, groundLayerMask))
            {
                return true;
            }
        }
        return false;
    }
    
    public void OnLook(InputValue value)
    {
        Vector2 lookInput = value.Get<Vector2>();
        CallLookEvent(lookInput);
    }

    public void OnRun(InputValue value)
    {
        isRun = value.isPressed;
        CallRunEvent();
    }

    public void OnAttack(InputValue value)
    {
        if (value.isPressed)
        {
            isAttacking = !isAttacking;
            CallAttackEvent();
        }
    }

    public void OnInteraction(InputValue value)
    {
        if (value.isPressed)
        {
            CallInteractEvent();
        }
    }
}


