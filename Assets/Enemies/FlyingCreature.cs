using GenericScripts;
using UnityEngine;

namespace Enemies
{
    public class FlyingCreature : LivingCreature
    {
        public override void TakeDamage(float damage)
        {
            _health -= damage;
            Debug.Log(_health.ToString());
            if (_health < 0)
                Die();
        }

        public override void Die()
        {
            ObjectPool.Instance.ReturnGameObjectToPool(gameObject);
        }
    }
}
