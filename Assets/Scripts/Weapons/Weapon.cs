using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // public WeaponData weaponData;
    public int attackDamage = 10; // -> WeaponData 생기면 없앤다
    public Collider weaponCollier;


    private bool isAttacking = false;

    private void Awake()
    {
        if (weaponCollier != null)
        {
            weaponCollier.enabled = false;
        }
    }
    private void Start()
    {
        if (weaponCollier != null)
        {
            weaponCollier.enabled = false;
        }
    }
    /*
    public void StartAttack()
    {
        if (weaponCollier != null)
        {
            weaponCollier.enabled = true;
            isAttacking = true;
        }
    }
    public void EndAttack()
    {
        if (weaponCollier != null)
        {
            weaponCollier.enabled = false;
            isAttacking = false;
        }
    }
    */
    private void OnTriggerEnter(Collider other)
    {
        if (isAttacking)
        {
            IDamagable damgable = other.GetComponent<IDamagable>();
            if (damgable != null)
            {
                damgable.TakeDamage(attackDamage, transform);
                // WeaponData 생기면 attackDamge ->  weaponData.attackDamge (변경)
            }
        }
    }
}
