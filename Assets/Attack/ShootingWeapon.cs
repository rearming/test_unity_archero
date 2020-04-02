using GenericScripts;
using UnityEngine;

namespace Attack
{
    public class ShootingWeapon : MonoBehaviour, IShooter
    {
        [SerializeField] protected bool overrideProjectileParams;
        [SerializeField] protected float weaponDamage;
        [SerializeField] protected float projectileSpeed;
        [SerializeField] protected GameObject projectilePrefab;

        private Transform _weaponTransform;

        void Start()
        {
            _weaponTransform = GetComponent<Transform>();
        }

        public void Shoot(Vector3 targetPos)
        {
            var projectileObject = ObjectPool.Instance.GetGameObjectFromPool(projectilePrefab);
            var projectileComponent = projectileObject.GetComponent<GenericProjectile>();
        
            projectileObject.transform.position = _weaponTransform.position;
            if (overrideProjectileParams)
                projectileComponent.OverrideProjectileParams(weaponDamage, projectileSpeed);
            projectileComponent.StartFlight(targetPos - projectileObject.transform.position);
        }
    }
}
