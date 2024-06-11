using System;
using UnityEngine;

public class PlayerController : MovementController
{
    public event Action OnAttackEvent;
    public event Action OnJumpEvent;
    public event Action OnRunEvent;
    public event Action OnInteractEvent;
    private float _timeSinceLastAttack = float.MaxValue;
    protected bool _isAttacking { get; set; }
    protected virtual void Update()
    {
        HandleAttackDelay();
    }
    private void HandleAttackDelay()
    {
        if (_timeSinceLastAttack <= 0.2f)
        {
            _timeSinceLastAttack += Time.deltaTime;
        }
        if (_isAttacking && _timeSinceLastAttack > 0.2f)
        {
            _timeSinceLastAttack = 0;
            CallAttackEvent();
        }
    }
    public void CallAttackEvent()
    {
        OnAttackEvent?.Invoke();
    }
    public void CallJumpEvent()
    {
        OnJumpEvent?.Invoke();
    }
    public void CallRunEvent()
    {
        OnRunEvent?.Invoke();
    }
    public void CallInteractEvent()
    {
        OnInteractEvent?.Invoke();
    }
}









