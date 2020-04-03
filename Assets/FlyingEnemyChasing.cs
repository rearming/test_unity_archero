using System.Collections;
using System.Collections.Generic;
using Enemies;
using UnityEngine;

public class FlyingEnemyChasing : StateMachineBehaviour
{
	[SerializeField] protected EnemyData _enemyData;
	private Transform _playerTransform;

	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
	    _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

     // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		var rawTargetPosition = Vector3.MoveTowards(animator.transform.position,
			_playerTransform.position, _enemyData.moveSpeed * Time.deltaTime);
		animator.transform.position = new Vector3(rawTargetPosition.x, animator.gameObject.transform.position.y, rawTargetPosition.z);
		animator.SetFloat("Distance", Vector3.Distance(animator.transform.position, _playerTransform.position));
	}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
