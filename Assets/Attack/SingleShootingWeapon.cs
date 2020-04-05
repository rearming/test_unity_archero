using GenericScripts;
using UnityEngine;

namespace Attack
{
    public sealed class SingleShootingWeapon : GenericShootingWeapon
    {
        [SerializeField] private float attacksPerSecond;
        public float AttacksPerSecond { get; private set; }
        
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
            var projectileObject = GetOverridenProjectileInstance();

            projectileObject.transform.position = WeaponTransform.position;
            projectileObject.GetComponent<GenericProjectile>().StartFlight(targetPos - projectileObject.transform.position);
        }
    }
}
