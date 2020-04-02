using System.Collections;
using System.Collections.Generic;
using System.Security;
using GenericScripts;
using Player;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    private Transform _transform;
    
    private PlayerData _playerData;
    private ShootingWeapon _shootingWeapon;
    
    [SerializeField] protected float attacksPerSecond;

    private LinkedList<Vector3> _enemiesPositions = new LinkedList<Vector3>();

    private float _timePast;
    
    private bool _rotationComplete;
    private float _rotationLerpValue;
    
    void Awake()
    {
        GetEnemiesPositions();
        _shootingWeapon = GetComponentInChildren<ShootingWeapon>();
        _playerData = GetComponent<PlayerData>();
        _transform = transform;
    }

    private void GetEnemiesPositions()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies)
        {
            Debug.Log(enemy.name);
            _enemiesPositions.AddFirst(enemy.GetComponentInChildren<Collider>().bounds.center);
        }
    }
    
    void Update()
    {
        if (_playerData.state == PlayerState.Idle)
            RotateToEnemy();
        if (_playerData.state == PlayerState.Moving)
            _rotationComplete = false;
        
        if (_rotationComplete)
        {
            _timePast += Time.deltaTime;
            if (_timePast >= attacksPerSecond)
            {
                _shootingWeapon.Shoot(_enemiesPositions.First.Value);
                _timePast = 0f;
            }
        }
    }

    void RotateToEnemy()
    {
        if (_rotationComplete)
            return;
        _transform.localEulerAngles = _playerData.rotator.SmoothLookAt(
            _transform.localEulerAngles.y,
            _enemiesPositions.First.Value - _transform.position,
            ref _rotationLerpValue);
        
        if (_rotationLerpValue > 1f)
        {
            _playerData.state = PlayerState.Shooting;
            _rotationComplete = true;
            _rotationLerpValue = 0;
        }
    }
}
