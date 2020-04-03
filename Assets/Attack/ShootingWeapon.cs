using GenericScripts;
using UnityEngine;

namespace Attack
{
    public class ShootingWeapon : MonoBehaviour, IShooter
    {
        [SerializeField] protected bool overrideProjectileParams;
        [SerializeField] protected float weaponDamage;
        [SerializeField] protected float projectileSpeed;
        public float attacksPerSecond;
        [SerializeField] protected GameObject projectilePrefab;

        private Transform _weaponTransform;
        [SerializeField] protected Collider ownerCollider; 

        void Start()
        {
            _weaponTransform = GetComponent<Transform>();
        }

        public void Shoot(Vector3 targetPos)
        {
            var projectileObject = ObjectPool.Instance.GetGameObjectFromPool(projectilePrefab);
            var projectileComponent = projectileObject.GetComponent<GenericProjectile>();
            
            projectileObject.transform.position = _weaponTransform.position;
            projectileComponent.ownerId = ownerCollider.GetInstanceID();
            if (overrideProjectileParams)
                projectileComponent.OverrideProjectileParams(weaponDamage, projectileSpeed);
            projectileComponent.StartFlight(targetPos - projectileObject.transform.position);
        }
    }
}
