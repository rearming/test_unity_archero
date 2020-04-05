using System;
using System.Collections.Generic;
using System.Linq;
using GenericScripts;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Security.Cryptography;
using EventType = GenericScripts.EventType;

namespace Enemies
{
    
    public class EnemiesController : MonoBehaviour
    {
        [SerializeField] private Vector3 upperLeftCorner;
        [SerializeField] private Vector3 lowerRightCorner;
        [SerializeField] private float spawnBoxSize;
        private List<Bounds> _spawnBounds = new List<Bounds>();
        
        [SerializeField] private GameObject flyingEnemyPrefab;
        [SerializeField] private int flyingEnemiesNumber;
        [SerializeField] private float flyingEnemySpawnHeight;
        
        [SerializeField] private GameObject groundEnemyPrefab;
        [SerializeField] private int groundEnemiesNumber;
        [SerializeField] private float groundEnemySpawnHeight;

        private int _spawnCellOffset;

        private List<Tuple<GameObject, Collider>> _enemies = new List<Tuple<GameObject, Collider>>();
        private int? _prevEnemyId;
        private int? _currEnemyId;

        private Transform _playerTransform;
    
        void Start()
        {
            _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            
            GetSpawnBoxes();
            Spawn(flyingEnemyPrefab, flyingEnemiesNumber, flyingEnemySpawnHeight);
            Spawn(groundEnemyPrefab, groundEnemiesNumber, groundEnemySpawnHeight);
        }
        
        private void GetSpawnBoxes()
        {
            for (float z = upperLeftCorner.z; z > lowerRightCorner.z; z -= spawnBoxSize)
            {
                for (float x = upperLeftCorner.x; x < lowerRightCorner.x; x += spawnBoxSize)
                {
                    var bounds = new Bounds(
                        new Vector3(x, 0, z),
                        new Vector3(spawnBoxSize, spawnBoxSize, spawnBoxSize) * 1.8f);
                    var ray = new Ray(new Vector3(x, 10, z), Vector3.down);
                    if (Physics.Raycast(ray, out var raycastHit)
                        && raycastHit.collider.gameObject.CompareTag("Ground"))
                        _spawnBounds.Add(bounds);
                }
            }
            _spawnBounds = _spawnBounds.OrderBy(item => Guid.NewGuid()).ToList();
        }

        private void Spawn(GameObject enemyPrefab, int enemiesNumber, float spawnHeight)
        {
            for (int i = _spawnCellOffset; i < enemiesNumber + _spawnCellOffset; i++)
            {
                if (i >= _spawnBounds.Count)
                    return;
                var newEnemy = ObjectPool.Instance.GetGameObjectFromPool(enemyPrefab);
                newEnemy.transform.parent = gameObject.transform;
                newEnemy.transform.position = new Vector3(_spawnBounds[i].center.x, spawnHeight, _spawnBounds[i].center.z);
                _enemies.Add(new Tuple<GameObject, Collider>(newEnemy, newEnemy.GetComponentInChildren<Collider>()));
            }
            _spawnCellOffset += enemiesNumber;
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
                EventManager.Instance.PostNostrification(EventType.Win, this);
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
        
        private void OnDrawGizmosSelected()
        {
            if (_spawnBounds.Count == 0)
                return;
            Gizmos.color = Color.green;
            foreach (var spawnBound in _spawnBounds)
            {
                var ray = new Ray(new Vector3(spawnBound.center.x, 10, spawnBound.center.z),
                    Vector3.down);
                Gizmos.DrawWireCube(spawnBound.center, spawnBound.extents);
            }
        }
    }
}
