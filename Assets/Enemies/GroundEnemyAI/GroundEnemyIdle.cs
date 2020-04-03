using System.Collections;
using System.Collections.Generic;
using GenericScripts;
using UnityEngine;

public class GroundEnemyIdle : StateMachineBehaviour
{
	[SerializeField] private float _meleeAttackDistance;
	private Transform _transform;
	private Transform _playerTransform;
    private float _timePassed;
	
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
	    _transform = animator.transform;
	    _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
	    _transform.LookAt(_playerTransform);
	    
	    Ray	ray = new Ray(_transform.position, _playerTransform.position - _transform.position);
	    if (Physics.Raycast(ray, out var hitInfo))
	    {
		    var distance = animator.GetFloat("Distance");
		    if (!hitInfo.collider.gameObject.CompareTag("Player"))
			    animator.SetTrigger("Chase");
		    else
		    {
			    if (distance <= _meleeAttackDistance)
				    animator.SetTrigger("Melee Attack");
			    else
				    animator.SetTrigger("Range Attack");
		    }
		    // animator.SetBool("IsChasing", true);
	    }
    }
}
