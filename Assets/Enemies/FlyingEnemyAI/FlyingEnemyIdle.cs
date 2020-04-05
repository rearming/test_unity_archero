using System.Collections;
using System.Collections.Generic;
using Attack;
using GameManager;
using GenericScripts;
using UnityEngine;
using EventType = GenericScripts.EventType;

public class FlyingEnemyIdle : StateMachineBehaviour
{
	private Transform _transform;
	private Transform _playerTransform;
	private ShootingWeapon _weapon;
	private float _timePassed;
	private bool _gameEnded;

	private bool _componentsCached;
	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		_timePassed = 0f;
		if (!_componentsCached)
		{
			_weapon = animator.gameObject.GetComponentInChildren<ShootingWeapon>();
			_playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
			_transform = animator.transform;
			EventManager.Instance.AddListener(EventType.Loose, (type, sender, o) => _gameEnded = true);
			_componentsCached = true;
		}
	}
	
	public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		_transform.LookAt(_playerTransform);
		_timePassed += Time.deltaTime;
	    if (_timePassed >= 1 / _weapon.attacksPerSecond && !_gameEnded)
	    {
		    animator.SetTrigger("Shoot");
		    _timePassed = 0f;
	    }
    }
}
