using System;
using GameManager;
using GenericScripts;
using UnityEngine;

namespace Enemies
{
    public class EnemyCreature : LivingCreature
    {
        private EnemyData _enemyData;
        private GameData _gameData;

        private void Start()
        {
            _enemyData = GetComponent<EnemyData>();
            _gameData = FindObjectOfType<GameData>();
        }

        public override void TakeDamage(float damage)
        {
            if (_health <= 0)
                return;
            _health -= damage;
            _enemyData.State = EnemyState.GetHit;
            if (_health <= 0)
                Die();
        }

        public override void Die()
        {
            _enemyData.State = EnemyState.Dying;
            _gameData.SendReward(_enemyData.GetReward());
        }
    }
}
