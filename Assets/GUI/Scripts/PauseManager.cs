using GenericScripts;
using UnityEngine;
using EventType = GenericScripts.EventType;

namespace GUI.Scripts
{
    public class PauseManager : MonoBehaviour
    {
        public void Awake()
        {
            Time.timeScale = 0;
        }

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
}
