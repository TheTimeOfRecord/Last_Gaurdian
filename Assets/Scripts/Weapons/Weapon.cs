using UnityEngine;

public class Weapon : MonoBehaviour
{
    public EquipmentItem equipmentItem;
    private void OnTriggerEnter(Collider other)
    {
        IDamagable damgable = other.GetComponent<IDamagable>();
        if (damgable != null)
        {
            damgable.TakeDamage((int)equipmentItem.attackDamage, transform);
        }
    }
}
