using Attack;
using Enemies;
using GenericScripts;
using UnityEngine;
using UnityEngine.iOS;
using UnityScript.Steps;
using EventType = GenericScripts.EventType;

namespace Player
{
    public class PlayerShoot : MonoBehaviour
    {
        private Transform _transform;
    
        private PlayerData _playerData;
        
        private SingleShootingWeapon _singleShootingWeapon;
        private TripleShootingWeapon _tripleShootingWeapon;
        private GenericShootingWeapon _currentShootingWeapon;

        private bool _stopShooting;
        private float _timePast;

        private bool _rotationComplete;
        private float _rotationLerpValue;
    
        private EnemiesController _enemiesController;

        void Awake()
        {
            _singleShootingWeapon = GetComponentInChildren<SingleShootingWeapon>();
            _tripleShootingWeapon = GetComponentInChildren<TripleShootingWeapon>();
            _currentShootingWeapon = _singleShootingWeapon;
            
            _playerData = GetComponent<PlayerData>();
            _transform = transform;

            _enemiesController = FindObjectOfType<EnemiesController>();
            EventManager.Instance.AddListener(EventType.Win, (eventType, sender, o) =>
            {
                _stopShooting = eventType == EventType.Win;
            });
        }
        
        void Update()
        {
            if (_playerData.State == PlayerState.Dead || _stopShooting)
                return;
            
            if (_playerData.State == PlayerState.Moving || _enemiesController.ClosestEnemyChanged())
                _rotationComplete = false;
            if (!_rotationComplete && _playerData.State == PlayerState.Idle)
                RotateToEnemy();
        
            SwitchWeapon();
            
            if (_rotationComplete)
            {
                _timePast += Time.deltaTime;
                if (_timePast >= 1f / _singleShootingWeapon.AttacksPerSecond)
                {
                    if (!_enemiesController.GetClosestEnemyPosition(out var closestEnemyPos)
                        || !EnemyVisible(closestEnemyPos))
                        return;
                    _playerData.State = PlayerState.Shooting;
                    _currentShootingWeapon.Shoot(closestEnemyPos);
                    _timePast = 0f;
                }
            }
        }

        void SwitchWeapon()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                _currentShootingWeapon = _singleShootingWeapon;
            else if (Input.GetKeyDown(KeyCode.Alpha2))
                _currentShootingWeapon = _tripleShootingWeapon;
        }

        private bool EnemyVisible(Vector3 closestEnemyPos)
        {
            var origin = transform.position;
            var ray = new Ray(origin, closestEnemyPos - origin);
            return Physics.Raycast(ray, out var raycastHit) && raycastHit.collider.CompareTag("Enemy");
        }
        
        void RotateToEnemy()
        {
            if (!_enemiesController.GetClosestEnemyPosition(out var closestEnemyPosition))
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
