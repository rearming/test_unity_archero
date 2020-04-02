using System;
using System.Collections.Generic;
using GenericScripts;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] protected GameObject[] enemyPrefabs;
        [SerializeField] protected Vector3[] hardcodedSpawnPositions;
    
        private LinkedList<Tuple<GameObject, Vector3>> _enemies = new LinkedList<Tuple<GameObject, Vector3>>();
        private int? _prevEnemyId;
        private int? _currEnemyId;

        private Transform _playerTransform;
    
        void Start()
        {
            _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            Spawn(0);
            Spawn(1);
        }

        private void Spawn(int index) // TODO сделать нормальный спаун
        {
            int enemyIndex = Random.Range(0, enemyPrefabs.Length - 1);
            var newEnemy = ObjectPool.Instance.GetGameObjectFromPool(enemyPrefabs[index]);
            newEnemy.transform.parent = gameObject.transform;
        
            newEnemy.transform.position = hardcodedSpawnPositions[index];
        
            var enemyColliderCenter = newEnemy.transform.position + GetComponentInChildren<Collider>().bounds.center;
            _enemies.AddFirst(new Tuple<GameObject, Vector3>(newEnemy, enemyColliderCenter));
        }
    
        public bool GetClosestEnemyPosition(out Vector3 closestEnemyPos)
        {
            var closestDistance = Mathf.Infinity;
            closestEnemyPos = Vector3.negativeInfinity;
            bool closestEnemyExists = false;
            int? closestEnemyId = null;

            foreach (var enemy in _enemies)
            {
                var distance = (enemy.Item2 - _playerTransform.position).magnitude;
                if (distance < closestDistance && enemy.Item1.activeSelf)
                {
                    closestDistance = distance;
                    closestEnemyPos = enemy.Item2;
                    closestEnemyExists = true;
                    closestEnemyId = enemy.Item1.GetInstanceID();
                }
            }
            _prevEnemyId = _currEnemyId;
            _currEnemyId = closestEnemyId;
            return closestEnemyExists;
        }

        public bool ClosestEnemyChanged()
        {
            return _prevEnemyId == null || _prevEnemyId != _currEnemyId;
        }
    }
}
