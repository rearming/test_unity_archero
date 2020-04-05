using System.Collections;
using System.Collections.Generic;
using Attack;
using GenericScripts;
using UnityEngine;

public sealed class TripleShootingWeapon : GenericShootingWeapon
{
	
    [SerializeField] private float attacksPerSecond;
    public float AttacksPerSecond { get; private set; }
    [SerializeField] private float angle;
    
	protected override void Start()
    {
	    base.Start();
	    AttacksPerSecond = attacksPerSecond;
	    OverrideProjectileParams();
    }
	
	protected override void OverrideProjectileParams()
	{
		OverridenProjectilePrefab = Instantiate(projectilePrefab);
		OverridenProjectilePrefab.name += this;
		var projectileComponent = OverridenProjectilePrefab.GetComponent<GenericProjectile>();
		projectileComponent.ignoredTag = ignoredTag;
		if (overrideProjectileParams)
			projectileComponent.OverrideProjectileParams(projectileDamage, projectileSpeed);
		ObjectPool.Instance.ReturnGameObjectToPool(OverridenProjectilePrefab);
	}

	protected override GameObject GetOverridenProjectileInstance()
	{
		return ObjectPool.Instance.GetGameObjectFromPool(OverridenProjectilePrefab);
	}
	
	public override void Shoot(Vector3 targetPos)
	{
		var projectileObjectCenter = GetOverridenProjectileInstance();
		var projectileObjectLeft = GetOverridenProjectileInstance();
		var projectileObjectRight = GetOverridenProjectileInstance();

		var originPos = WeaponTransform.position;
		projectileObjectCenter.transform.position = originPos;
		projectileObjectLeft.transform.position = originPos;
		projectileObjectRight.transform.position = originPos;
		
		var targetDirCenter = targetPos - projectileObjectCenter.transform.position;
		var targetDirLeft = Quaternion.Euler(0, -angle, 0) * targetDirCenter;
		var targetDirRight = Quaternion.Euler(0, angle, 0) * targetDirCenter;
		
		Debug.DrawRay(originPos, targetDirLeft, Color.blue, 1f);
		Debug.DrawRay(originPos, targetDirCenter, Color.red, 1f);
		Debug.DrawRay(originPos, targetDirRight, Color.green, 1f);

		projectileObjectCenter.GetComponent<GenericProjectile>().StartFlight(targetDirCenter);
		projectileObjectLeft.GetComponent<GenericProjectile>().StartFlight(targetDirLeft);
		projectileObjectRight.GetComponent<GenericProjectile>().StartFlight(targetDirRight);
	}
}
