using System;
using GameManager;
using GenericScripts;
using UnityEngine;

namespace Enemies
{
    public class EnemyCreature : LivingCreature
    {
        private EnemyData _enemyData;

        private void Start()
        {
            _enemyData = GetComponent<EnemyData>();
        }

        public override void TakeDamage(float damage)
        {
            _health -= damage;
            if (_health < 0)
                Die();
        }

        public override void Die()
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameData>().SendReward(_enemyData.GetReward());
            ObjectPool.Instance.ReturnGameObjectToPool(gameObject);
        }
    }
}
