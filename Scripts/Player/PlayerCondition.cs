using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void TakePhysicalDamage(int damage);
}

public class PlayerCondition : MonoBehaviour, IDamageable
{
    public Condition health;
    public Condition hunger;
    public Condition stamina;

    public float noHungerHealthDecay;

    public event Action onTakeDamage;

    private PlayerInputController _playerInputController; //get isRun

    private void Awake()
    {
        _playerInputController = GetComponent<PlayerInputController>();
    }

    public void Update()
    {
        hunger.Subtract(hunger.RegebRate * Time.deltaTime);

        if (!_playerInputController.isRun)
        {
            stamina.Add(stamina.RegebRate * Time.deltaTime);
        }

        if (_playerInputController.isRun && stamina.CurValue > 0)
        {
            stamina.Subtract(stamina.RegebRate * 10 * Time.deltaTime);
        }
        else if (_playerInputController.isRun && stamina.CurValue <= 0)
        {
            stamina.CurValue = 0;
            _playerInputController.isRun = false;
        }

        if (hunger.CurValue == 0f)
        {
            health.Subtract(noHungerHealthDecay * Time.deltaTime);
        }

        if (health.CurValue == 0f)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        health.Add(amount);
    }

    public void Eat(float amount)
    {
        hunger.Add(amount);
    }

    public void Die()
    {
        EndingManager.Instance.CheckEnding(EndingType.PlayerDie);
    }

    public void TakePhysicalDamage(int damage)
    {
        SoundManager.Instance.Play(2, 0.5f);
        health.Subtract(damage);
        onTakeDamage?.Invoke();
    }

    public bool UseStamina(float amount)
    {
        if (stamina.CurValue - amount <= 0f)
        {
            return false;
        }

        stamina.Subtract(amount);
        return true;
    }

    public void ChangehungerRegebRate(float amount)
    {
        hunger.RegebRate = amount;
    }
}
