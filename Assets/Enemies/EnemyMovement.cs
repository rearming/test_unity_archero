using UnityEngine;

namespace Enemies
{
    public class EnemyMovement : MonoBehaviour
    {
        public virtual void Move()
        {
            Debug.Log($"Move() not implemented in {gameObject}!");
        }
    }
}
