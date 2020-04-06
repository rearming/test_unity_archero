using GenericScripts;
using UnityEngine;

namespace Attack
{
	public sealed class TripleShootingWeapon : GenericShootingWeapon
	{
		public override string WeaponType => nameof(TripleShootingWeapon);
	
		[SerializeField] private float angle;
    
		protected override void Start()
		{
			base.Start();
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

			projectileObjectCenter.GetComponent<GenericProjectile>().StartFlight(targetDirCenter);
			projectileObjectLeft.GetComponent<GenericProjectile>().StartFlight(targetDirLeft);
			projectileObjectRight.GetComponent<GenericProjectile>().StartFlight(targetDirRight);
		}
	}
}
