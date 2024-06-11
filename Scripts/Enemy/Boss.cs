using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public enum BossSkill
{
    Attack,
    HeartStrike
}

public class Boss : Enemy
{
    [Header("Boss")]
    [SerializeField] private BossSkill _bossSkill;
    [SerializeField] private float _bossSkillDistance;

    [Header("HeartStrike Skill")]
    [SerializeField] private GameObject _heartBullet;
    [SerializeField] private Transform _bulletPos;
    [SerializeField] private float _bulletNum;
    [SerializeField] private float _heartStrikeDistance;
    [SerializeField] private float _heartStrikeCoolTime;

    private bool _isAttacking = false;

    protected override void AttackPlayer()
    {
        base.AttackPlayer();
        if (!_isAttacking)
        {
            _bossSkill = SetSkill();
            _isAttacking = true;
        }
        
        if (_playerToEnemyDistance > _bossSkillDistance || !IsPlayerInFieldOfView())
        {
            agent.isStopped = false;
            NavMeshPath path = new NavMeshPath();
            if (agent.CalculatePath(CharacterManager.Instance.Player.transform.position, path))
            {
                agent.SetDestination(CharacterManager.Instance.Player.transform.position);
            }
        }
        else if (_playerToEnemyDistance < _bossSkillDistance)
        {
            if (Time.time - lastAttackTime > _attackCooltime)
            {
                lastAttackTime = Time.time;
                UseSkill(_bossSkill);
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

    BossSkill SetSkill()
    {
        int _id = Random.Range(0, System.Enum.GetValues(typeof(BossSkill)).Length);
        switch (_id)
        {
            case 0:
                _bossSkillDistance = _attackDistance;
                return BossSkill.Attack;
            case 1:
                _bossSkillDistance = _heartStrikeDistance;
                return BossSkill.HeartStrike;
        }
        return BossSkill.Attack;
    }

    void UseSkill(BossSkill skill)
    {
        switch(skill)
        {
            case BossSkill.Attack:
                Attack();
                break;
            case BossSkill.HeartStrike:
                HeartStrike();
                break;
            default:
                break;
        }
    }

    void Attack()
    {
        animator.speed = 1;
        animationController.AttackAnim();
        agent.isStopped = true;
        _isAttacking = false;
    }

    void HeartStrike()
    {
        agent.isStopped = true;
        StartCoroutine(HeartStrikeCoroutine());
    }

    IEnumerator HeartStrikeCoroutine()
    {
        float _nowBullet = _bulletNum;

        while (_nowBullet > 0)
        {
            ShootHeartBullet();
            _nowBullet--;
            yield return new WaitForSeconds(_heartStrikeCoolTime);  
        }
        _isAttacking = false;
    }

    void ShootHeartBullet()
    {
        Instantiate(_heartBullet, _bulletPos.position, Quaternion.identity);
        _heartBullet.GetComponent<HeartBullet>().SetDamage((int)Data.AttackPower);
    }

    protected override void Die()
    {
        base.Die();
        EndingManager.Instance.CheckEnding(EndingType.KillBoss);
    }
}