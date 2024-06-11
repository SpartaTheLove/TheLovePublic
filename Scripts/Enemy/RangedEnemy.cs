using UnityEngine;
using UnityEngine.AI;

public class RangedEnemy : Enemy
{
    [SerializeField] private GameObject _heartBullet;
    [SerializeField] private Transform _bulletPos;

    protected override void AttackPlayer()
    {
        base.AttackPlayer();
        if (_playerToEnemyDistance > _attackDistance || !IsPlayerInFieldOfView())
        {
            agent.isStopped = false;
            NavMeshPath path = new NavMeshPath();
            if (agent.CalculatePath(CharacterManager.Instance.Player.transform.position, path))
            {
                agent.SetDestination(CharacterManager.Instance.Player.transform.position);
            }
        }
        else if (_playerToEnemyDistance < _attackDistance)
        {
            if (Time.time - lastAttackTime > _attackCooltime)
            {
                lastAttackTime = Time.time;
                animator.speed = 1;
                animationController.AttackAnim();
                agent.isStopped = true;
            }
        }
    }

    public void Shoot()
    {
        Instantiate(_heartBullet, _bulletPos.position, Quaternion.identity) ;
        _heartBullet.GetComponent<HeartBullet>().SetDamage((int)Data.AttackPower);
    }
}