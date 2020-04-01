﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] protected float moveSpeed;

    private Rigidbody _rigidbody;
    private Transform _transform;

    private Vector3 _currMovementDir;
    private Vector3 _prevMovementDir;
    
    [SerializeField] private float _rotationSpeed = 0.1f;
    
    // если угол между прошлым и текущим векторами движения равен 180 градусам,
    // поворот игрока в произойдет четыре раза медленнее  
    private const float AngleDiffRotationSlower = 4;
    private const float AngleDiffRotationSlowerCoeff = 180 / AngleDiffRotationSlower;
    
    private float _lerpValue;

    private PlayerData _playerData;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _transform = transform;
        _playerData = GetComponent<PlayerData>();
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
            _playerData.state = PlayerState.Moving;
        else if (_lerpValue == 0f)
            _playerData.state = PlayerState.Idle;
    }
    
    void UpdateMovementRotation()
    {
        if (_currMovementDir != Vector3.zero)
            _prevMovementDir = _currMovementDir;
        
        var prevRotation = _transform.localEulerAngles.y;
        var newRotation = Mathf.Atan2(_prevMovementDir.x, _prevMovementDir.z) * Mathf.Rad2Deg;

        var angleDiff = Mathf.Abs(Mathf.DeltaAngle(prevRotation, newRotation));
        newRotation = Mathf.LerpAngle(prevRotation, newRotation, _lerpValue);
        _lerpValue += _rotationSpeed / (angleDiff / AngleDiffRotationSlowerCoeff);
        if (_lerpValue > 1f)
            _lerpValue = 0;
        
        _transform.localEulerAngles = new Vector3(0, newRotation);
    }
    
    void UpdateMovementDir()
    {
        _currMovementDir.x = Input.GetAxis("Horizontal");
        _currMovementDir.z = Input.GetAxis("Vertical");
    }
}
