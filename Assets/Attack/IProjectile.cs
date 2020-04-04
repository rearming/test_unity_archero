using GenericScripts;
using UnityEngine;

namespace Attack
{
	public interface IProjectile : IDamageArea
	{
		void StartFlight(Vector3 flightDir);
	}
}