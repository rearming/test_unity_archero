using Attack;
using Enemies;
using UnityEngine;

namespace Player
{
    public class PlayerShoot : MonoBehaviour
    {
        private Transform _transform;
    
        private PlayerData _playerData;
        private ShootingWeapon _shootingWeapon;
    
        [SerializeField] protected float attacksPerSecond;

        private float _timePast;
    
        private bool _rotationComplete;
        private float _rotationLerpValue;
    
        private EnemySpawner _enemySpawner;
    
        void Awake()
        {
            _shootingWeapon = GetComponentInChildren<ShootingWeapon>();
            _playerData = GetComponent<PlayerData>();
            _transform = transform;

            _enemySpawner = GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<EnemySpawner>();
        }

        void Update()
        {
            if (_playerData.state == PlayerState.Moving || _enemySpawner.ClosestEnemyChanged())
                _rotationComplete = false;
            if (!_rotationComplete && _playerData.state == PlayerState.Idle)
                RotateToEnemy();
        
            if (_rotationComplete)
            {
                _timePast += Time.deltaTime;
                if (_timePast >= attacksPerSecond)
                {
                    Vector3 closestEnemyPos;
                    if (!_enemySpawner.GetClosestEnemyPosition(out closestEnemyPos))
                        return;
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
                _playerData.state = PlayerState.Shooting;
                _rotationComplete = true;
                _rotationLerpValue = 0;
            }
        }
    }
}
