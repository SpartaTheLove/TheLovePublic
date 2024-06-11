using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimController : AnimationController
{
    BowInputSystem _bowInputSystem;

    private Camera mainCam;

    protected override void Awake()
    {
        base.Awake();
        mainCam = Camera.main;
    }

    private void Start()
    {
        _bowInputSystem = GetComponent<BowInputSystem>();
    }

    public void IsJump()
    {
        animator.SetTrigger("IsJump");
    }

    public void Aim(bool aiming)
    {
        animator.SetBool("Aim", aiming);
    }

    public void PullString(bool pull)
    {
        animator.SetBool("Aim", true);
        animator.SetBool("PullString", pull);
    }

    public void FireArrow()
    {
        animator.SetTrigger("FireArrow");
    }
}