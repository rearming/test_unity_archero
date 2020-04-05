using System.Collections;
using System.Collections.Generic;
using Enemies;
using GenericScripts;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private EnemyData _enemyData;
    private Animator _animator;
    
    void Start()
    {
        _animator = GetComponent<Animator>();
        _enemyData = GetComponent<EnemyData>();
        EventManager.Instance.AddListener(EVENT_TYPE.Loose, (type, sender, o) => _animator.SetTrigger("Win"));
    }
    
    void Update()
    {
        if (_enemyData.State == EnemyState.Dying)
            _animator.SetTrigger("Die");
        if (_enemyData.State == EnemyState.GetHit)
        {
            _animator.SetTrigger("GetHit");
            _enemyData.State = _enemyData.GetPrevState();
        }
    }
}
