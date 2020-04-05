using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenericScripts
{
    public enum EVENT_TYPE
    {
        Win,
        Loose,
        Pause,
        Resume,
    };

    public class EventManager : MonoBehaviour
    {
        public static EventManager Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        public delegate void OnEvent(EVENT_TYPE eventType, Component sender, object param = null);

        private Dictionary<EVENT_TYPE, List<OnEvent>> 
            _listeners = new Dictionary<EVENT_TYPE, List<OnEvent>>();
        
        public void AddListener(EVENT_TYPE eventType, OnEvent listener)
        {
            if (_listeners.TryGetValue(eventType, out var listenList))
            {
                listenList.Add(listener);
                return;
            }

            listenList = new List<OnEvent>();
            listenList.Add(listener);
            _listeners.Add(eventType, listenList);
        }

        public void PostNostrification(EVENT_TYPE eventType, Component sender, object param = null)
        {
            if (!_listeners.TryGetValue(eventType, out var listenList))
                return;
            foreach (var eventListener in listenList)
                eventListener(eventType, sender, param);
        }
    }
}