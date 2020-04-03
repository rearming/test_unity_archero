using System.Collections;
using System.Collections.Generic;
using Enemies;
using UnityEngine;

public class FlyingEnemyChasing : StateMachineBehaviour
{
	[SerializeField] protected EnemyData _enemyData;
	private Transform _playerTransform;
	private Transform _transform;

	private bool _componentsCached;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
	    if (!_componentsCached)
	    {
		    _transform = animator.transform;
			_playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
			_componentsCached = true;
	    }
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
	    _transform.LookAt(_playerTransform);
	    var rawTargetPosition = Vector3.MoveTowards(animator.transform.position,
			_playerTransform.position, _enemyData.moveSpeed * Time.deltaTime);
		animator.transform.position = new Vector3(rawTargetPosition.x, animator.gameObject.transform.position.y, rawTargetPosition.z);
	}
}
