using System.Collections;
using System.Collections.Generic;
using Enemies;
using UnityEngine;

public class FlyingEnemyAnimation : MonoBehaviour
{
    private EnemyData _enemyData;
    
    void Start()
    {
        _enemyData = GetComponent<EnemyData>();
    }
}
