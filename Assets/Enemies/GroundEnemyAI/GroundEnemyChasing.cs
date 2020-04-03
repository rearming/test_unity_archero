using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GroundEnemyChasing : StateMachineBehaviour
{
	private Transform _playerTransform;
	private NavMeshAgent _navMeshAgent;
	
	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		_playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
		_navMeshAgent = animator.gameObject.GetComponent<NavMeshAgent>();
	}
	
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
	    _navMeshAgent.SetDestination(_playerTransform.position);
    }
}
