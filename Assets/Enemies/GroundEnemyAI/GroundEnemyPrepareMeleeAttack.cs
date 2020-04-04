using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemyPrepareMeleeAttack : StateMachineBehaviour
{
    private Transform _playerTransform;
    
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
    
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.LookAt(_playerTransform);
    }
}
