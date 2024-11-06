using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    private PlayerAnimationManager animationManager;

    [Header("Attack Settings")]
    public float comboTimeWindow = 0.5f;
    public float thirdAttackCoolTime = 1.7f;
    
    private int currrentAttackIndex = 0;
    private bool isComboActive = false;
    private bool isCooldown = false;
    private bool isAttacking = false;

    private void Start()
    {
        animationManager = GetComponent<PlayerAnimationManager>();

        PlayerController playerController = GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.onAttackInput += HandleAttackInput;
        }
    }

    private void HandleAttackInput()
    {
        if (isCooldown || isAttacking)
        {
            return;
        }

        StartCoroutine(AttackMotion());
    }
    private IEnumerator AttackMotion()
    {
        if (!isComboActive)
        {
            isComboActive = true;
            currrentAttackIndex = 0;
        }

        isAttacking = true;
        animationManager.TriggerAttack();

        yield return new WaitForSeconds(0.5f);

        isAttacking = false;

        currrentAttackIndex++;
        if (currrentAttackIndex >= 3)
        {
            ResetCombo();
            StartCoroutine(StartCooldown());
        }
        yield return new WaitForSeconds(comboTimeWindow);
    }
    private IEnumerator StartCooldown()
    {
        isCooldown = true;
        yield return new WaitForSeconds(thirdAttackCoolTime);
        isCooldown = false;
    }

    private void ResetCombo()
    {
        currrentAttackIndex = 0;
        isComboActive = false;
    }
}
