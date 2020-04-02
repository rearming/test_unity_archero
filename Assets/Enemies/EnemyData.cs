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
		[HideInInspector]
		public EnemyState state;
		private void Awake()
		{
			state = EnemyState.Idle;
		}
	}
}
