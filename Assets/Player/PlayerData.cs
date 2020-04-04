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
		GetHit,
		Dead
	}
	public class PlayerData : MonoBehaviour
	{
		private PlayerState _prevState;
		private PlayerState _state;
		[HideInInspector]
		public PlayerState State
		{
			get => _state;
			set
			{
				_prevState = _state;
				_state = value;
			}
		}
		
		public float moveSpeed;
		
		[SerializeField] private float rotationSpeed = 0.1f;
		[SerializeField] private float rotationSlower = 4f;

		public SmoothRotator rotator;

		private void Awake()
		{
			rotator = new SmoothRotator(rotationSpeed, rotationSlower);
			State = PlayerState.Idle;
		}
		
		public PlayerState GetPrevState()
		{
			return _prevState;
		}
	}
}
