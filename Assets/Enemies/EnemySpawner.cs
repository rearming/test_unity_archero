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
    
        private List<Tuple<GameObject, Collider>> _enemies = new List<Tuple<GameObject, Collider>>();
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
            
            _enemies.Add(new Tuple<GameObject, Collider>(newEnemy, newEnemy.GetComponentInChildren<Collider>()));
        }
    
        public bool GetClosestEnemyPosition(out Vector3 closestEnemyPos)
        {
            var closestDistance = Mathf.Infinity;
            closestEnemyPos = Vector3.negativeInfinity;
            bool closestEnemyExists = false;
            int? closestEnemyId = null;
            
            if (NoMoreEnemies())
                return false;

            for (int i = _enemies.Count - 1; i >= 0; i--)
            {
                if (RemoveEnemyIfDead(_enemies[i], i))
                    continue;
                var enemyPos = _enemies[i].Item2.bounds.center;
                var distance = Vector3.Distance(enemyPos, _playerTransform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemyPos = enemyPos;
                    closestEnemyExists = true;
                    closestEnemyId = _enemies[i].Item1.GetInstanceID();
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
        
        private bool NoMoreEnemies()
        {
            if (_enemies.Count == 0)
            {
                EventManager.Instance.PostNostrification(EVENT_TYPE.Win, this);
                return true;
            }
            return false;
        }
        private bool RemoveEnemyIfDead(Tuple<GameObject, Collider> enemy, int i)
        {
            if (!enemy.Item1.activeSelf)
            {
                _enemies.RemoveAt(i);
                return true;
            }
            return false;
        }
    }
}
