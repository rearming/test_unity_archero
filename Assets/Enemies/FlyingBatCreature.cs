using System.Collections;
using System.Collections.Generic;
using GenericScripts;
using UnityEngine;

public class FlyingBatCreature : LivingCreature
{
    public override void TakeDamage(float damage)
    {
        _health -= damage;
        if (_health < 0)
            Die();
    }

    public override void Die()
    {
        base.Die();
    }
}
