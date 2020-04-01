using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
	Idle,
	Moving,
	Shooting
}
public class PlayerData : MonoBehaviour
{
	public PlayerState state;

	private void Awake()
	{
		state = PlayerState.Idle;
	}
}
