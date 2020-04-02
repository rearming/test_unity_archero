using GenericSctipts;
using UnityEngine;

namespace Attack
{
	public interface IProjectile
	{
		void StartFlight(Vector3 flightDir);
		void DealDamage(LivingCreature target);
	}
}