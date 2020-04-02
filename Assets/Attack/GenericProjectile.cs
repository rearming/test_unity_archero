using System.Collections;
using System.Collections.Generic;
using Attack;
using GenericSctipts;
using UnityEngine;

public class GenericProjectile : MonoBehaviour, IProjectile
{
    [SerializeField] protected Rigidbody rigidbody;
    
    [SerializeField] protected float _damage;
    [SerializeField] protected float _speed;

    public virtual void StartFlight(Vector3 flightDir)
    {
        Debug.Log("StartFlight on projectile not implemented!");
    }
    
    public virtual void UpdateProjectileParams(float damage, float speed)
    {
        _damage = damage;
        _speed = speed;
    }
    
    public virtual void UpdateProjectileParams(float damage)
    {
        _damage = damage;
    }
    
    public virtual void DealDamage(LivingCreature target)
    {
        Debug.Log("DealDamage not implemented! (Call)");
    }
}
