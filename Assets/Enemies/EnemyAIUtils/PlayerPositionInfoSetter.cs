using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPositionInfoSetter : MonoBehaviour
{
    private Transform _playerTransform;
    private Animator _animator;
    
    void Start()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        _animator = GetComponent<Animator>();
    }
    
    void Update()
    {
        _animator.SetFloat("Distance", 
            Vector3.Distance(_animator.transform.position, _playerTransform.position));   
    }
}
