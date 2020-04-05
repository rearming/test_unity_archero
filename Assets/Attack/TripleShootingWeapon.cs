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
		OverridenProjectilePrefab = projectilePrefab;
		var projectileComponent = OverridenProjectilePrefab.GetComponent<GenericProjectile>();
		projectileComponent.ignoredTag = ignoredTag;
		if (overrideProjectileParams)
			projectileComponent.OverrideProjectileParams(projectileDamage, projectileSpeed);
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
		
		var targetDir = targetPos - projectileObjectCenter.transform.position;
		var targetDirLeft = Quaternion.Euler(0, -angle, 0);
		var targetDirRight = Quaternion.Euler(0, angle, 0);

		var originPos = WeaponTransform.position;
		projectileObjectCenter.transform.position = originPos;
		projectileObjectLeft.transform.position = originPos;
		projectileObjectRight.transform.position = originPos;
		
		projectileObjectCenter.GetComponent<GenericProjectile>().StartFlight(targetDir);
		projectileObjectLeft.GetComponent<GenericProjectile>().StartFlight(targetDir);
		projectileObjectRight.GetComponent<GenericProjectile>().StartFlight(targetDir);
	}
}
