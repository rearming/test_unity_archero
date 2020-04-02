using System.Collections;
using System.Collections.Generic;
using GenericSctipts;
using UnityEngine;

public class StoneProjectile : GenericProjectile
{
	public override void StartFlight(Vector3 flightDir)
	{
		Debug.Log("flight started: " + (flightDir * _speed).ToString());
		rigidbody.AddForce(flightDir * _speed);
	}

	public override void DealDamage(LivingCreature target)
	{
		target.TakeDamage(_damage);
	}
}
