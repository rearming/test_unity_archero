using UnityEngine;

namespace Enemies.FlyingEnemyAI
{
	public class FlyingEnemyChasing : StateMachineBehaviour
	{
		private Transform _playerTransform;
	
		private EnemyData _enemyData;
		private Transform _transform;
		private Rigidbody _rigidbody;

		private bool _componentsCached;
		public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			if (!_componentsCached)
			{
				_transform = animator.transform;
				_playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
				_enemyData = animator.gameObject.GetComponent<EnemyData>();
				_rigidbody = animator.gameObject.GetComponentInChildren<Rigidbody>();
				_componentsCached = true;
			}
		}

		public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			_transform.LookAt(_playerTransform);
			var rawTargetPosition = Vector3.MoveTowards(animator.transform.position,
				_playerTransform.position, _enemyData.moveSpeed * Time.deltaTime);
			_rigidbody.MovePosition(new Vector3(rawTargetPosition.x, animator.gameObject.transform.position.y, rawTargetPosition.z));
		}
	}
}
