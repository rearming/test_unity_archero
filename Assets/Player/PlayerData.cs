using System;
using System.Collections;
using System.Collections.Generic;
using GenericScripts;
using UnityEngine;

namespace Player
{
	public enum PlayerState
	{
		Idle,
		Moving,
		Shooting,
		Dead
	}
	public class PlayerData : MonoBehaviour
	{
		[HideInInspector]
		public PlayerState state;
		
		public float moveSpeed;
		
		[SerializeField] private float rotationSpeed = 0.1f;
		[SerializeField] private float rotationSlower = 4f;

		public SmoothRotator rotator;

		private void Awake()
		{
			rotator = new SmoothRotator(rotationSpeed, rotationSlower);
			state = PlayerState.Idle;
		}
	}
}
