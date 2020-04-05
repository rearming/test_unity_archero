using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;
using GenericScripts;
using EventType = GenericScripts.EventType;

namespace Player
{
	public class PlayerCreature : LivingCreature
	{
		private PlayerData _playerData;
		public override void Awake()
		{
			base.Awake();
			_playerData = GetComponent<PlayerData>();
		}

		public override void TakeDamage(float damage)
		{
			Health -= damage;
			_playerData.State = PlayerState.GetHit;
			EventManager.Instance.PostNostrification(EventType.PlayerHit, this);
			if (Health <= 0)
				Die();
		}
	
		public override void Die()
		{
			if (_playerData.State != PlayerState.Dead)
			{
				_playerData.State = PlayerState.Dead;
				EventManager.Instance.PostNostrification(EventType.Loose, this);
			}
		}
	}
}
