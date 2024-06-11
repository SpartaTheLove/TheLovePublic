using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour
{
    private Enemy enemy;

    private void Awake()
    {
        enemy = GetComponentInParent<MeleeEnemy>();
        if (enemy == null ) enemy = GetComponentInParent<Boss>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6 && enemy._canAttack)
        {
            CharacterManager.Instance.Player.condition.TakePhysicalDamage((int)enemy.Data.AttackPower);
            enemy._canAttack = false;
        }
    }
}
