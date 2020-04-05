using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenericScripts
{
	public class LivingCreature : MonoBehaviour, IMortal
	{
		[SerializeField] protected float health; // for inspector
		private float _health;
		public float Health
		{
			get => _health;
			protected set
			{
				_deltaHealth = value - _health;
				_health = value;
			}
		}
		private float _deltaHealth;

		public float DeltaHealth => _deltaHealth;

		public virtual void Awake()
		{
			Health = health;
		}

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
