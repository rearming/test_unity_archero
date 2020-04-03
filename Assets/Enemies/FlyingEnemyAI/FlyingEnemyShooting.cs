using System.Collections;
using System.Collections.Generic;
using Attack;
using UnityEngine;

public class FlyingEnemyShooting : StateMachineBehaviour
{
	private ShootingWeapon _weapon;

	private bool _componentsCached;
	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if (!_componentsCached)
		{
			_weapon = animator.gameObject.GetComponentInChildren<ShootingWeapon>();
			_weapon.Shoot(GameObject.FindGameObjectWithTag("Player").transform.position);
			_componentsCached = true;
		}
	}
}
