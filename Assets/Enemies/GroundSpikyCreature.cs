using System.Collections;
using System.Collections.Generic;
using GenericScripts;
using UnityEngine;

public class GroundSpikyCreature : LivingCreature
{
    public override void TakeDamage(float damage)
    {
        _health -= damage;
        if (_health < 0)
            Die();
    }

    public override void Die()
    {
        Debug.Log($"{gameObject} is dead");
        ObjectPool.Instance.ReturnGameObjectToPool(gameObject);
    }
}
