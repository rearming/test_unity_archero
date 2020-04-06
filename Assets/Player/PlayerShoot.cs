using System.Collections.Generic;
using System.Diagnostics;
using Attack;
using Enemies;
using GenericScripts;
using UnityEngine;
using UnityEngine.iOS;
using UnityScript.Steps;
using Debug = UnityEngine.Debug;
using EventType = GenericScripts.EventType;

namespace Player
{
    public class PlayerShoot : MonoBehaviour
    {
        private Transform _transform;
    
        private PlayerData _playerData;
        
        [SerializeField] private GenericShootingWeapon[] weaponsObjects;
        private Dictionary<string, GenericShootingWeapon> _weapons = new Dictionary<string, GenericShootingWeapon>();
        private GenericShootingWeapon _currentShootingWeapon;

        private bool _stopShooting;
        private float _timePast;

        private bool _rotationComplete;
        private float _rotationLerpValue;
    
        private EnemiesController _enemiesController;

        void Awake()
        {
            _playerData = GetComponent<PlayerData>();
            _transform = transform;

            _enemiesController = FindObjectOfType<EnemiesController>();
            AddWeapons();
            SubscribeOnEvents();
        }

        private void SubscribeOnEvents()
        {
            EventManager.Instance.AddListener(EventType.Win, (eventType, sender, o) =>
                { _stopShooting = eventType == EventType.Win; });
            EventManager.Instance.AddListener(EventType.WeaponChanged, (type, sender, param) =>
            {
                var weaponName = param as string;
                if (weaponName == null || !_weapons.TryGetValue(weaponName, out _currentShootingWeapon))
                    Debug.Log($"Unknown weapon name {weaponName}!");
            });
        }
        
        private void AddWeapons()
        {
            _currentShootingWeapon = weaponsObjects[0]; // default weapon
            foreach (var weaponsObject in weaponsObjects)
                _weapons.Add(weaponsObject.WeaponType, weaponsObject);
        }
        
        void Update()
        {
            if (_playerData.State == PlayerState.Dead || _stopShooting)
                return;
            
            if (_playerData.State == PlayerState.Moving || _enemiesController.ClosestEnemyChanged())
                _rotationComplete = false;
            if (!_rotationComplete && _playerData.State == PlayerState.Idle)
                RotateToEnemy();

            if (_rotationComplete)
            {
                _timePast += Time.deltaTime;
                if (_timePast >= 1f / _currentShootingWeapon.AttacksPerSecond)
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
