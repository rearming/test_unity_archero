using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using GenericScripts;
using UnityEngine;
using EventType = GenericScripts.EventType;

public class PauseManager : MonoBehaviour
{
    public void Pause()
    {
        EventManager.Instance.PostNostrification(EventType.Pause, this);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        EventManager.Instance.PostNostrification(EventType.Resume, this);
        Time.timeScale = 1;
    }
}
