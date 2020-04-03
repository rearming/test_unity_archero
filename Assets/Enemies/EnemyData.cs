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
		Hiding,
		Dying
	}
	
	public class EnemyData : MonoBehaviour
	{
		[SerializeField] protected int reward;
	
		[HideInInspector]
		public EnemyState state;

		public float moveSpeed;
		
		private void Awake()
		{
			state = EnemyState.Idle;
		}

		public int GetReward()
		{
			return reward;
		}
	}
}
