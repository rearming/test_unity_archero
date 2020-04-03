using System.Collections;
using System.Collections.Generic;
using Attack;
using UnityEngine;

public class GroundEnemyRangeAttack : StateMachineBehaviour
{
    private ShootingWeapon _weapon; 
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _weapon = animator.gameObject.GetComponentInChildren<ShootingWeapon>();
        _weapon.Shoot(GameObject.FindGameObjectWithTag("Player").transform.position);
    }
}
