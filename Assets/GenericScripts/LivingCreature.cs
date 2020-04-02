using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenericScripts
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
