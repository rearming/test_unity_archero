using UnityEngine;

namespace Enemies.GroundEnemyAI
{
    public class GroundEnemyPrepareMeleeAttack : StateMachineBehaviour
    {
        private Transform _playerTransform;

        private bool _componentsCached;
    
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (!_componentsCached)
            {
                _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
                _componentsCached = true;
            }
        }
    
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.transform.LookAt(_playerTransform);
        }
    }
}
