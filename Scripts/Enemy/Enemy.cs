using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Enemy : MonoBehaviour
{
    [SerializeField] public EnemyData Data;

    protected Condition health;
    protected AnimationController animationController;
    protected Animator animator;
    protected NavMeshAgent agent;
    protected SkinnedMeshRenderer[] meshRenderers;

    [Header("Detect")]
    [SerializeField] private CharacterState _state;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private float _detectDistance;
    [SerializeField] private float _detectCoolTime;

    protected float _playerToEnemyDistance;

    [Header("Walk")]
    [SerializeField] private float _minWalkDistance;
    [SerializeField] private float _maxWalkDistance;

    [Header("Attack")]
    [SerializeField] protected float _attackDistance;
    [SerializeField] protected float _attackCooltime;
    [SerializeField] protected float _viewAngle;

    protected float lastAttackTime;

    public bool _canAttack;

    protected void Start()
    {
        health = GetComponent<Condition>();
        agent = GetComponent<NavMeshAgent>();
        animationController = GetComponent<AnimationController>();
        animator = animationController.animator;
        meshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();

        SetState(CharacterState.Walk);
    }

    protected void Update()
    {
        _playerToEnemyDistance = Vector3.Distance(transform.position, CharacterManager.Instance.Player.transform.position);

        if(_playerToEnemyDistance >= _detectDistance) SetState(CharacterState.Walk);

        animationController.SetAnimator(_state);

        if (agent.isActiveAndEnabled)
        {
            switch (_state)
            {
                case CharacterState.Idle:
                    DetectPlayer();
                    break;
                case CharacterState.Walk:
                    DetectPlayer();
                    break;
                case CharacterState.Run:
                    AttackPlayer();
                    break;
            }
        }
    }

    protected void SetState(CharacterState aiState)
    {
        if (!agent.isActiveAndEnabled || !agent.isOnNavMesh) return;

        _state = aiState;

        switch (_state)
        {
            case CharacterState.Idle:
                agent.speed = Data.WalkSpeed;
                agent.isStopped = true;
                break;
            case CharacterState.Walk:
                agent.speed = Data.WalkSpeed;
                agent.isStopped = false;
                break;
            case CharacterState.Run:
                agent.speed = Data.RunSpeed;
                break;
        }

        animator.speed = agent.speed / Data.WalkSpeed;
    }

    void DetectPlayer()
    {
        if (!agent.isActiveAndEnabled || !agent.isOnNavMesh) return;

        if (_state == CharacterState.Walk && agent.remainingDistance < 0.1f)
        {
            SetState(CharacterState.Idle);
            animationController.SetAnimator(CharacterState.Idle);
            Invoke("WalkToNewLocation", _detectCoolTime);
        }

        if (_playerToEnemyDistance < _detectDistance)
        {
            SetState(CharacterState.Run);
        }
    }

    void WalkToNewLocation()
    {
        if (_state != CharacterState.Idle)
        {
            return;
        }
        SetState(CharacterState.Walk);
        agent.SetDestination(GetDestination());
    }

    Vector3 GetDestination()
    {
        NavMeshHit hit;

        int i = 0;
        do
        {
            NavMesh.SamplePosition(transform.position + (Random.onUnitSphere * Random.Range(_minWalkDistance, _maxWalkDistance)), out hit, _maxWalkDistance, NavMesh.AllAreas);
            i++;
            if (i == 30)
                break;
        } while (Vector3.Distance(transform.position, hit.position) < _detectDistance);

        return hit.position;
    }

    protected virtual void AttackPlayer()
    {
        RotateToPlayer();
    }

    void RotateToPlayer()
    {
        Vector3 _dir = CharacterManager.Instance.Player.transform.position - transform.position;
        _dir.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(_dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.deltaTime);
    }

    protected bool IsPlayerInFieldOfView()
    {
        Vector3 directionToPlayer = CharacterManager.Instance.Player.transform.position - transform.position;
        float angle = Vector3.Angle(transform.forward, directionToPlayer);
        return angle < _viewAngle * 0.5f;
    }

    public void TakePhysicalDamage(int amount)
    {
        health.CurValue -= amount;
        if (health.CurValue <= 0)
        {
            Die();
            QuestManager.Instance.CallCheckSlayMonsterQuest(this);
        }
        StartCoroutine(DamageFlash());
    }

    IEnumerator DamageFlash()
    {
        for (int x = 0; x < meshRenderers.Length; x++)
            meshRenderers[x].material.color = new Color(1.0f, 0.6f, 0.6f);

        yield return new WaitForSeconds(0.1f);

        for (int x = 0; x < meshRenderers.Length; x++)
            meshRenderers[x].material.color = Color.white;
    }

    protected virtual void Die()
    {
        for (int i = 0; i < Data.DropItem.Length; i++)
        {
            Instantiate(Data.DropItem[i], transform.position + Vector3.forward * 2, Quaternion.identity);
        }
        agent.enabled = false;
        animationController.DieAnim();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 6)
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 pushDirection = (transform.position - collision.transform.position).normalized;
                pushDirection.y = 0;

                rb.AddForce(pushDirection * 5f, ForceMode.Impulse);
            }
        }
    }
}
