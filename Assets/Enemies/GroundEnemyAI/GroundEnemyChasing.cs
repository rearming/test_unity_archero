using System.Collections;
using System.Collections.Generic;
using Attack;
using UnityEngine;
using UnityEngine.AI;

public class GroundEnemyChasing : StateMachineBehaviour
{
	private Transform _playerTransform;
	private Vector3 _playerPosition;

	private Vector3 _raycastOrigin;
	private NavMeshAgent _navMeshAgent;
	private Transform _transform;
	
	private bool _componentsCached;
	
	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if (!_componentsCached)
		{
			_transform = animator.transform;
			_playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
			_navMeshAgent = animator.gameObject.GetComponent<NavMeshAgent>();
			_raycastOrigin = animator.gameObject.GetComponentInChildren<ShootingWeapon>().gameObject.transform.position;
			_componentsCached = true;
		}
		_playerPosition = _playerTransform.position;
	}
	
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
	    _transform.LookAt(_playerPosition);
	    Ray ray = new Ray(_raycastOrigin, _playerTransform.position - _raycastOrigin);
	    if (Physics.Raycast(ray, out var hitInfo))
	    {
		    if (hitInfo.collider.gameObject.CompareTag("Player"))
			    animator.SetBool("IsChasing", false);
	    }
	    _navMeshAgent.SetDestination(_playerPosition);
    }
}
