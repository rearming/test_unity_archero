using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using GenericScripts;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] protected GameObject[] showOnPauseObjects;
    [SerializeField] protected GameObject[] hideOnPauseObjects;
    
    void Start()
    {
        EventManager.Instance.AddListener(EVENT_TYPE.Pause, (type, sender, o) =>
        {
            foreach (var pauseObject in showOnPauseObjects) pauseObject.SetActive(true);
            foreach (var pauseObject in hideOnPauseObjects) pauseObject.SetActive(false);
        });
        
        EventManager.Instance.AddListener(EVENT_TYPE.Resume, (type, sender, o) =>
        {
            foreach (var pauseObject in showOnPauseObjects) pauseObject.SetActive(false);
            foreach (var pauseObject in hideOnPauseObjects) pauseObject.SetActive(true);
        });
    }
}
