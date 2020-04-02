using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public virtual void Move()
    {
        Debug.Log($"Move() not implemented in {this}!");
    }
}
