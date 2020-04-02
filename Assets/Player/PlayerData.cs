using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
	public enum PlayerState
	{
		Idle,
		Moving,
		PrepareAttack,
		Shooting,
		Dying
	}
	public class PlayerData : MonoBehaviour
	{
		[HideInInspector]
		public PlayerState state;

		private void Awake()
		{
			state = PlayerState.Idle;
		}
	}
}
