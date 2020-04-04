using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;
using GenericScripts;

namespace Player
{
	public class PlayerCreature : LivingCreature
	{
		private PlayerData _playerData;
		private void Start()
		{
			_playerData = GetComponent<PlayerData>();
		}

		public override void TakeDamage(float damage)
		{
			_health -= damage;
			_playerData.State = PlayerState.GetHit;
			if (_health <= 0)
				Die();
		}
	
		public override void Die()
		{
			_playerData.State = PlayerState.Dead;
		}
	}
}
