using System.Collections;
using System.Collections.Generic;
using Attack;
using GenericScripts;
using UnityEngine;

public class FlyingEnemyShooting : StateMachineBehaviour
{
	private Transform _playerTransform; 
	private ShootingWeapon _weapon;

	private bool _componentsCached;
	
	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if (!_componentsCached)
		{
			_weapon = animator.gameObject.GetComponentInChildren<ShootingWeapon>();
			_playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
			_componentsCached = true;
		}
		_weapon.Shoot(_playerTransform.position);
	}
}
