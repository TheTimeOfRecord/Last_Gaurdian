using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    private Animator animator;

    public void Initialize(Animator animator)
    {
        this.animator = animator;
    }

    // �̵�
    public void SetMovementAnimation(Vector2 moveDirection)
    {
        if (animator == null)
        {
            return;
        }
        animator.SetBool("isRunFront", moveDirection.y > 0);
        animator.SetBool("isRunBack", moveDirection.y < 0);
        animator.SetBool("isRunLeft", moveDirection.x < 0);
        animator.SetBool("isRunRight", moveDirection.x > 0);
    }

    // ����
    public void TriggerJump()
    {
        animator.SetTrigger("isJump");
    }

    // ����
    public void TriggerAttack()
    {
        animator.SetTrigger("isAttack");
    }

    // ���� ���(?)
    public void SetShield(bool isShielding)
    {
        animator.SetBool("isShield", isShielding);
    }
}
