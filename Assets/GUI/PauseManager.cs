using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using GenericScripts;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public void Pause()
    {
        EventManager.Instance.PostNostrification(EVENT_TYPE.Pause, this);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        EventManager.Instance.PostNostrification(EVENT_TYPE.Resume, this);
        Time.timeScale = 1;
    }
}
