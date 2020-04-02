using System.Collections;
using System.Collections.Generic;
using Generic_Scripts;
using UnityEngine;

namespace GenericSctipts
{
	public class LivingCreature : MonoBehaviour, IMortal
	{
		[SerializeField] protected float _health;

		public virtual void TakeDamage(float damage)
		{
			Debug.Log("Taking damage!");
		}

		public virtual void Die()
		{
			Debug.Log("Dead!");
		}
	}
}
