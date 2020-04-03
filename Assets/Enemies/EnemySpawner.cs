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
    
        private LinkedList<Tuple<GameObject, Collider>> _enemies = new LinkedList<Tuple<GameObject, Collider>>();
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
            
            _enemies.AddFirst(new Tuple<GameObject, Collider>(newEnemy, newEnemy.GetComponentInChildren<Collider>()));
        }
    
        public bool GetClosestEnemyPosition(out Vector3 closestEnemyPos)
        {
            var closestDistance = Mathf.Infinity;
            closestEnemyPos = Vector3.negativeInfinity;
            bool closestEnemyExists = false;
            int? closestEnemyId = null;

            foreach (var enemy in _enemies)
            {
                var enemyPos = enemy.Item2.bounds.center;
                var distance = Vector3.Distance(enemyPos, _playerTransform.position);
                if (distance < closestDistance && enemy.Item1.activeSelf)
                {
                    closestDistance = distance;
                    closestEnemyPos = enemyPos;
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
