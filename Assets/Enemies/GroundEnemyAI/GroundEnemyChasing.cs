using System.Collections;
using System.Collections.Generic;
using Attack;
using UnityEngine;
using UnityEngine.AI;

public class GroundEnemyChasing : StateMachineBehaviour
{
	private Transform _playerTransform;
	private Vector3 _playerPosition;
	
	private NavMeshAgent _navMeshAgent;
	private Transform _weaponTransform;

	private float _timePast;
	
	private bool _componentsCached;
	
	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if (!_componentsCached)
		{
			_playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
			_navMeshAgent = animator.gameObject.GetComponent<NavMeshAgent>();
			_weaponTransform = animator.gameObject.GetComponentInChildren<ShootingWeapon>().gameObject.transform;
			_componentsCached = true;
		}
		_playerPosition = _playerTransform.position;
		_navMeshAgent.SetDestination(_playerPosition);
	}
	
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
	    var raycastOrigin = _weaponTransform.position;
	    Ray ray = new Ray(raycastOrigin, _playerTransform.position - raycastOrigin);
	    if (Physics.Raycast(ray, out var hitInfo))
	    {
		    if (hitInfo.collider.gameObject.CompareTag("Player"))
		    {
			    animator.SetBool("IsChasing", false);
			    _navMeshAgent.ResetPath();
		    }
	    }
    }
}
