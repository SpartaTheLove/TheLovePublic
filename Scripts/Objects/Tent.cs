using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tent : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float _healAmount;

    [Header("Hunger")]
    [SerializeField] private float _hungerRegebRate;

    [Header("Values")]
    [SerializeField] private float _invokeInterval;
    [SerializeField] private float _originalhungerRegebRate;
    [SerializeField] private GameObject _promptText;

    private void Awake()
    {
        _originalhungerRegebRate = CharacterManager.Instance.Player.condition.hunger.RegebRate;
    }

    void AddHealth()
    {
        CharacterManager.Instance.Player.condition.Heal(_healAmount);
    }

    void DelaySubtractHunger()
    {
        CharacterManager.Instance.Player.condition.ChangehungerRegebRate(_hungerRegebRate);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer ==6)
        {
            _promptText.SetActive(true);
            InvokeRepeating("AddHealth", 0, _invokeInterval);
            DelaySubtractHunger();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            _promptText.SetActive(false);
            CancelInvoke("AddHealth");
            CharacterManager.Instance.Player.condition.ChangehungerRegebRate(_originalhungerRegebRate);
        }
    }

}
