using System.Collections;
using System.Collections.Generic;
using Attack;
using GenericScripts;
using UnityEngine;
using UnityEngine.AI;

public class GroundEnemyIdle : StateMachineBehaviour
{
	private ShootingWeapon _weapon;
	[SerializeField] private float _meleeAttackDistance;
	private Transform _transform;
	private Transform _playerTransform;
    private float _timePassed;

    private bool _componentsCached;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
	    _timePassed = 0f;
	    if (!_componentsCached)
	    {
		    _weapon = animator.gameObject.GetComponentInChildren<ShootingWeapon>();
		    _transform = animator.transform;
		    _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
		    _componentsCached = true;
	    }
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
	    _transform.LookAt(_playerTransform);
	    _timePassed += Time.deltaTime;
	    
	    Ray	ray = new Ray(_transform.position, _playerTransform.position - _transform.position);
	    if (Physics.Raycast(ray, out var hitInfo))
	    {
		    if (!hitInfo.collider.gameObject.CompareTag("Player") && !hitInfo.collider.isTrigger)
			    animator.SetBool("IsChasing", true);
		    else
		    {
			    var distance = animator.GetFloat("Distance");
			    if (distance <= _meleeAttackDistance)
				    animator.SetTrigger("Melee Attack");
			    else if (_timePassed >= 1f / _weapon.attacksPerSecond)
			    {
				    animator.SetTrigger("Range Attack");
				    _timePassed = 0f;
			    }
		    }
	    }
	    
    }
}
