using GenericScripts;
using UnityEngine;

namespace Attack
{
	public class StoneProjectile : GenericProjectile
	{
		public override void StartFlight(Vector3 flightDir)
		{
			rigidbody.AddForce(flightDir * _speed);
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.GetInstanceID() == ownerId || other.isTrigger)
				return;
			DealDamage(other.gameObject.GetComponentInParent<LivingCreature>());
			ObjectPool.Instance.ReturnGameObjectToPool(gameObject);
		}

		public override void DealDamage(LivingCreature target)
		{
			if (target != null)
				target.TakeDamage(_damage);
		}
	}
}
