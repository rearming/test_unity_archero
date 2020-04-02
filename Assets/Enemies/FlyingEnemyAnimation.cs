using UnityEngine;

namespace Enemies
{
    public class FlyingEnemyAnimation : MonoBehaviour
    {
        private EnemyData _enemyData;
    
        void Start()
        {
            _enemyData = GetComponent<EnemyData>();
        }
    }
}
