﻿using System.Collections;
using System.Collections.Generic;
 using Player;
 using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] protected float moveSpeed;

    private Rigidbody _rigidbody;
    private Transform _transform;

    private Vector3 _currMovementDir;
    private Vector3 _prevMovementDir;
    
    [SerializeField] private float _rotationSpeed = 0.1f;
    private float _rotationSlower = 4f;

    private float _rotationLerpValue;
    private bool _rotationComplete;

    private PlayerData _playerData;
    private PlayerRotator _playerRotator;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _transform = transform;
        _playerData = GetComponent<PlayerData>();
        _playerRotator = new PlayerRotator(_rotationSpeed, _rotationSlower);
    }
    
    void Update()
    {
        UpdateMovementDir();
        UpdateMovementRotation();
        UpdatePlayerState();
        _rigidbody.velocity = _currMovementDir * moveSpeed;
    }

    void UpdatePlayerState()
    {
        if (_currMovementDir != Vector3.zero)
        {
            _playerData.state = PlayerState.Moving;
            _rotationComplete = false;
        }
        else if (_rotationComplete)
            _playerData.state = PlayerState.Idle;
    }
    
    void UpdateMovementRotation()
    {
        if (_rotationComplete)
            return;
        if (_currMovementDir != Vector3.zero)
            _prevMovementDir = _currMovementDir;
        
        _transform.localEulerAngles =
            _playerRotator.SmoothLookAt(_transform.localEulerAngles.y, _prevMovementDir, ref _rotationLerpValue);

        if (_rotationLerpValue > 1f)
        {
            _rotationComplete = true;
            _rotationLerpValue = 0;
        }
    }
    
    void UpdateMovementDir()
    {
        _currMovementDir.x = Input.GetAxis("Horizontal");
        _currMovementDir.z = Input.GetAxis("Vertical");
    }
}
