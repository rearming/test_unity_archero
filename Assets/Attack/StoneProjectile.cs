using System;
using System.Collections;
using System.Collections.Generic;
using GenericScripts;
using UnityEngine;

public class StoneProjectile : GenericProjectile
{
	public override void StartFlight(Vector3 flightDir)
	{
		rigidbody.AddForce(flightDir * _speed);
	}

	private void OnTriggerEnter(Collider other)
	{
		DealDamage(other.gameObject.GetComponent<LivingCreature>());
		ObjectPool.Instance.ReturnGameObjectToPool(gameObject);
	}

	public override void DealDamage(LivingCreature target)
	{
		if (target != null)
			target.TakeDamage(_damage);
	}
}
