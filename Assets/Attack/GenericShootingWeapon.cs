using System;
using System.Collections;
using System.Collections.Generic;
using Attack;
using UnityEngine;

public class GenericShootingWeapon : MonoBehaviour, IShootingWeapon
{
    [SerializeField] protected GameObject projectilePrefab;
    
    protected GameObject OverridenProjectilePrefab;
    [SerializeField] protected bool overrideProjectileParams;
    [SerializeField] protected float projectileDamage;
    [SerializeField] protected float projectileSpeed;
    
    [SerializeField] protected string ignoredTag;
    
    protected Transform WeaponTransform;

    protected virtual void Start()
    {
        WeaponTransform = GetComponent<Transform>();
    }

    protected virtual void OverrideProjectileParams() { }
    
    protected virtual GameObject GetOverridenProjectileInstance() => null;

    public virtual void Shoot(Vector3 targetPos)
    {
        Debug.Log($"{nameof(Shoot)} is not implemented in {name}!");
    }
}
