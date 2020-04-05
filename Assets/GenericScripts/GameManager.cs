using System.Collections;
using System.Collections.Generic;
using Enemies;
using UnityEngine;

namespace GenericScripts
{
    public class GameManager : MonoBehaviour
    {
        void Start()
        {
            EventManager.Instance.AddListener(EventType.Win, GameWon);
        }
        
        private void GameWon(EventType eventType, Component sender, object param = null)
        {
            Debug.Log("Game Won!");
        }
    }
}
