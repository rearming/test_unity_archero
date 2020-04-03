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
			if (other.CompareTag(ignoredTag))
				return;
			var target = other.gameObject.GetComponentInParent<LivingCreature>();
			if (target != null)
				DealDamage(target);
			ObjectPool.Instance.ReturnGameObjectToPool(gameObject);
		}

		public override void DealDamage(LivingCreature target)
		{
			if (target != null)
				target.TakeDamage(_damage);
		}
	}
}
