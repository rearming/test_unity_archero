using System.Collections;
using System.Collections.Generic;
using Attack;
using GenericScripts;
using UnityEngine;

public class GenericProjectile : MonoBehaviour, IProjectile
{
    #pragma warning disable CS0108
    [SerializeField] protected Rigidbody rigidbody;
    #pragma warning restore
    
    [SerializeField] protected float _damage;
    [SerializeField] protected float _speed;

    public virtual void StartFlight(Vector3 flightDir)
    {
        Debug.Log($"StartFlight() on {gameObject} not implemented!");
    }

    public virtual void DealDamage(LivingCreature target)
    {
        Debug.Log($"DealDamage() on {gameObject} not implemented!");
    }
    
    public virtual void OverrideProjectileParams(float damage, float speed)
    {
        _damage = damage;
        _speed = speed;
    }
    
    public virtual void OverrideProjectileParams(float damage)
    {
        _damage = damage;
    }
    
    private void OnDisable()
    {
        ResetParams();
    }
    
    private void ResetParams()
    {
        rigidbody.velocity = Vector3.zero;
    }
}
