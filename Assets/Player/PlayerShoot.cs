using Attack;
using Enemies;
using GenericScripts;
using UnityEngine;

namespace Player
{
    public class PlayerShoot : MonoBehaviour
    {
        private Transform _transform;
    
        private PlayerData _playerData;
        private ShootingWeapon _shootingWeapon;

        private bool _stopShooting;
        private float _timePast;

        private bool _rotationComplete;
        private float _rotationLerpValue;
    
        private EnemySpawner _enemySpawner;

        void Awake()
        {
            _shootingWeapon = GetComponentInChildren<ShootingWeapon>();
            _playerData = GetComponent<PlayerData>();
            _transform = transform;

            _enemySpawner = FindObjectOfType<EnemySpawner>();
            EventManager.Instance.AddListener(EVENT_TYPE.Win, (eventType, sender, o) =>
            {
                _stopShooting = eventType == EVENT_TYPE.Win;
            });
        }

        void Update()
        {
            if (_playerData.State == PlayerState.Dead || _stopShooting)
                return;
            
            if (_playerData.State == PlayerState.Moving || _enemySpawner.ClosestEnemyChanged())
                _rotationComplete = false;
            if (!_rotationComplete && _playerData.State == PlayerState.Idle)
                RotateToEnemy();
        
            if (_rotationComplete)
            {
                _timePast += Time.deltaTime;
                if (_timePast >= 1f / _shootingWeapon.attacksPerSecond)
                {
                    if (!_enemySpawner.GetClosestEnemyPosition(out var closestEnemyPos))
                        return;
                    _playerData.State = PlayerState.Shooting;
                    _shootingWeapon.Shoot(closestEnemyPos);
                    _timePast = 0f;
                }
            }
        }
        
        void RotateToEnemy()
        {
            Vector3 closestEnemyPosition;
            if (!_enemySpawner.GetClosestEnemyPosition(out closestEnemyPosition))
                return;
        
            _transform.localEulerAngles = _playerData.rotator.SmoothLookAt(
                _transform.localEulerAngles.y,
                closestEnemyPosition - _transform.position,
                ref _rotationLerpValue);
        
            if (_rotationLerpValue > 1f)
            {
                _rotationComplete = true;
                _rotationLerpValue = 0;
            }
        }
    }
}
