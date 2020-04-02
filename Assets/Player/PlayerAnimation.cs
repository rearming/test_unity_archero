using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerAnimation : MonoBehaviour
{
    private PlayerData _playerData;
    private PlayerRotator _playerRotator;

    private Animator _animator;

    private Transform _transform;

    private float _rotationLerpValue;
    private bool _rotationCompleted;

    private Vector3 _enemyPos;
    
    void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _transform = transform;
        _playerData = GetComponent<PlayerData>();
        _playerRotator = new PlayerRotator(0.1f, 4);
    }
    
    void Update()
    {
        _animator.SetBool("Moving", _playerData.state == PlayerState.Moving);
        if (Input.GetMouseButtonDown((int) MouseButton.LeftMouse))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                _enemyPos = hit.point;
                _rotationCompleted = false;
            }
        }
        RotateToEnemy(_enemyPos);
    }

    void RotateToEnemy(Vector3 enemyPos)
    {
        if (_rotationCompleted)
            return;
        _transform.localEulerAngles = _playerRotator.SmoothLookAt(_transform.localEulerAngles.y,
            enemyPos - _transform.position, ref _rotationLerpValue);
        if (_rotationLerpValue > 1f)
        {
            _rotationLerpValue = 0;
            _rotationCompleted = true;
        }
    }
}
