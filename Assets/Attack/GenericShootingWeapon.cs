using UnityEngine;

namespace Attack
{
    public class GenericShootingWeapon : MonoBehaviour, IShootingWeapon
    {
        public virtual string WeaponType => null;
    
        [SerializeField] protected GameObject projectilePrefab;
    
        protected GameObject OverridenProjectilePrefab;
        [SerializeField] protected bool overrideProjectileParams;
        [SerializeField] protected float projectileDamage;
        [SerializeField] protected float projectileSpeed;
        [SerializeField] private float attacksPerSecond;
        public float AttacksPerSecond { get; private set; }

        [SerializeField] protected string ignoredTag;
    
        protected Transform WeaponTransform;

        protected virtual void Start()
        {
            WeaponTransform = GetComponent<Transform>();
            AttacksPerSecond = attacksPerSecond;
        }

        protected virtual void OverrideProjectileParams() { }
    
        protected virtual GameObject GetOverridenProjectileInstance() => null;

        public virtual void Shoot(Vector3 targetPos)
        {
            Debug.Log($"{nameof(Shoot)} is not implemented in {name}!");
        }
    }
}
