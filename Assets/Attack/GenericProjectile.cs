using System.Collections;
using GenericScripts;
using UnityEngine;

namespace Attack
{
    public class GenericProjectile : MonoBehaviour, IProjectile
    {
        [HideInInspector]
        public string ignoredTag;
#pragma warning disable CS0108
        [SerializeField] private Rigidbody rigidbody;
#pragma warning restore
    
        [SerializeField] protected float damage;
        [SerializeField] protected float speed;

        public virtual void StartFlight(Vector3 flightDir)
        {
            Debug.Log($"StartFlight() on {gameObject} not implemented!");
        }

        public virtual void DealDamage(LivingCreature target)
        {
            Debug.Log($"DealDamage() on {gameObject} not implemented!");
        }
    
        public virtual void OverrideProjectileParams(float newDamage, float newSpeed)
        {
            this.damage = newDamage;
            this.speed = newSpeed;
        }
    
        public virtual void OverrideProjectileParams(float newDamage)
        {
            this.damage = newDamage;
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
}
