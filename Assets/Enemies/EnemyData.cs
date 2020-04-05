using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
	public enum EnemyState
	{
		Idle,
		Moving,
		Shooting,
		GetHit,
		Dying
	}
	
	public class EnemyData : MonoBehaviour
	{
		[SerializeField] private int reward;

		private EnemyState _prevState;
		private EnemyState _state;
		[HideInInspector]
		public EnemyState State
		{
			get => _state;
			set
			{
				_prevState = _state;
				_state = value;
			}
		}

		public float moveSpeed;
		
		private void Awake()
		{
			State = EnemyState.Idle;
		}

		public int GetReward()
		{
			return reward;
		}

		public EnemyState GetPrevState()
		{
			return _prevState;
		}
	}
}
