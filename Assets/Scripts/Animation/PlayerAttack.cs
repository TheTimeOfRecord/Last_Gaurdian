using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    private PlayerAnimationManager animationManager;
    private Weapon weapon;

    [Header("Attack Settings")]
    [SerializeField] private float comboTimeWindow = 0.5f;
    [SerializeField] private float thirdAttackCoolTime = 1.7f;


    private int currrentAttackIndex = 0;
    private bool isComboActive = false;
    private bool isCooldown = false;
    private bool isAttacking = false;

    private void Start()
    {
        animationManager = GetComponent<PlayerAnimationManager>();
        weapon = GetComponentInChildren<Weapon>();

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
