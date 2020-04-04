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
            EventManager.Instance.AddListener(EVENT_TYPE.Win, GameWon);
        }
        
        private void GameWon(EVENT_TYPE eventType, Component sender, object param = null)
        {
            Debug.Log("Game Won!");
        }
    }
}
