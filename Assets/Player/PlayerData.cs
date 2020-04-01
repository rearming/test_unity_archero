using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
	Idle,
	Moving,
	PrepareAttack,
	Shooting
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
