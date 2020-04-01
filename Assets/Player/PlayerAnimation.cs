using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private PlayerData _playerData;
    private Animator _animator;
    
    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _playerData = GetComponent<PlayerData>();
        Debug.Log(_playerData.ToString());
    }
    
    void Update()
    {
        _animator.SetBool("Moving", _playerData.state == PlayerState.Moving);
        // if (_playerData.state == PlayerState.Idle && _animator.GetBool("Moving"))
        //     _animator.SetBool("Moving", false);
        // if (_playerData.state == PlayerState.Moving && !_animator.GetBool("Moving"))
        //     _animator.SetBool("Moving", true);
    }
}
