using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;
using GenericSctipts;

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
		}
	
		public override void Die()
		{
			_playerData.state = PlayerState.Dying;
		}
	}
}
