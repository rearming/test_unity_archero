using System.Collections;
using System.Collections.Generic;
using System.Security;
using Attack;
using GenericScripts;
using UnityEngine;

public class DamageArea : MonoBehaviour, IDamageArea
{
    [SerializeField] protected string ignoredTag;
    [SerializeField] protected float damage; 
        
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
        Debug.Log(target.name);
        target.TakeDamage(damage);
    }
}
