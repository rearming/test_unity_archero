using System.Collections;
using System.Collections.Generic;
using GenericScripts;
using UnityEngine;

public class EnemyDie : StateMachineBehaviour
{
    private float _timePast;
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _timePast += Time.deltaTime;
        if (_timePast >= stateInfo.length)
            OnStateExit(animator, stateInfo, layerIndex);
        // help Unity to call this function when no translation further :)
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ObjectPool.Instance.ReturnGameObjectToPool(animator.gameObject);
    }
}