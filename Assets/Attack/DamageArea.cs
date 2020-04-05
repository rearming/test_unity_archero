using System.Collections;
using System.Collections.Generic;
using System.Security;
using Attack;
using GenericScripts;
using UnityEngine;

public class DamageArea : MonoBehaviour, IDamageArea
{
    [SerializeField] private string ignoredTag;
    [SerializeField] private float damage; 
        
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag(ignoredTag))
            return;
        var target = other.gameObject.GetComponentInParent<LivingCreature>();
        if (target != null)
            DealDamage(target);
    }

    public void DealDamage(LivingCreature target)
    {
        target.TakeDamage(damage);
    }
}
