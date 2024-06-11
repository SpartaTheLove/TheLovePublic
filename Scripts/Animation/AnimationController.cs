using System.Collections;
using UnityEngine;
    
public enum CharacterState
{
    Idle,
    Walk,
    Run,
    Attack,
    Die
}

public class AnimationController : MonoBehaviour
{
    [SerializeField]
    public Animator animator;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }
    }

    public void SetAnimator(CharacterState state)
    {
        switch (state)
        {
            case CharacterState.Idle:
                animator.SetBool("IsWalk", false);
                animator.SetBool("IsRun", false);
                break;
            case CharacterState.Walk:
                animator.SetBool("IsWalk", true);
                animator.SetBool("IsRun", false);
                break;
            case CharacterState.Run:
                animator.SetBool("IsWalk", false);
                animator.SetBool("IsRun", true);
                break;
        }
    }

    public void AttackAnim()
    {
        animator.SetTrigger("Attack");
    }

    public void DieAnim()
    {
        animator.SetTrigger("Die");
        StartCoroutine(DisableAfterAnimation());
    }

    IEnumerator DisableAfterAnimation()
    {
        yield return new WaitForSeconds(5f);
        animator.enabled = false;
        // TODO :: 사라지는 파티클
        gameObject.SetActive(false);
    }

    public void IsWalk(Vector2 isWalk)
    {
        bool isMoving = isWalk != Vector2.zero;
        animator.SetBool("IsWalk", isMoving);
    }

    public void IsRun(bool isRunning)
    {
        animator.SetBool("IsRun", isRunning);
    }
}