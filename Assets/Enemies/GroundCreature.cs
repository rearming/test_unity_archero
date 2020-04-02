using GenericScripts;

namespace Enemies
{
    public class GroundCreature : LivingCreature
    {
        public override void TakeDamage(float damage)
        {
            _health -= damage;
            if (_health < 0)
                Die();
        }

        public override void Die()
        {
            ObjectPool.Instance.ReturnGameObjectToPool(gameObject);
        }
    }
}
