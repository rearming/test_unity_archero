using System.Collections;
using GenericScripts;
using UnityEngine;

namespace Attack
{
	public class StoneProjectile : GenericProjectile
	{
		public override void StartFlight(Vector3 flightDir)
		{
			GetComponent<Rigidbody>().AddForce(flightDir * speed);
			StartCoroutine(nameof(LateDestruct));
		}

		private IEnumerator LateDestruct()
		{
			yield return new WaitForSeconds(5f);
			ObjectPool.Instance.ReturnGameObjectToPool(gameObject);
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.CompareTag(ignoredTag) || other.isTrigger)
				return;
			var target = other.gameObject.GetComponentInParent<LivingCreature>();
			if (target != null)
				DealDamage(target);
			ObjectPool.Instance.ReturnGameObjectToPool(gameObject);
		}

		public override void DealDamage(LivingCreature target)
		{
			if (target != null)
				target.TakeDamage(damage);
		}
	}
}
