using UnityEngine;
using System.Collections;

public class MeleeEnemy : Enemy
{
    protected override void AttackPlayer()
    {
        base.AttackPlayer();
        if (_playerToEnemyDistance > _attackDistance || !IsPlayerInFieldOfView())
        {
            agent.isStopped = false;
            UnityEngine.AI.NavMeshPath path = new UnityEngine.AI.NavMeshPath();
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
    public void CanMeleeAttack()
    {
        _canAttack = true;
        StartCoroutine(WaitSeconds(1f));
    }
    IEnumerator WaitSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        _canAttack = false;
    }
}