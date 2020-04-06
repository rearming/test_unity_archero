using GenericScripts;
using UnityEngine;
using EventType = GenericScripts.EventType;

namespace GUI.Scripts
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private GameObject[] showOnPauseObjects;
        [SerializeField] private GameObject[] hideOnPauseObjects;
    
        void Start()
        {
            HideObjects(true); // game starts from pause
        
            EventManager.Instance.AddListener(EventType.Pause, (type, sender, o) => { HideObjects(true); });
            EventManager.Instance.AddListener(EventType.Resume, (type, sender, o) => { HideObjects(false); });
        }

        private void HideObjects(bool isPause)
        {
            foreach (var pauseObject in showOnPauseObjects)
                pauseObject.SetActive(isPause);
            foreach (var pauseObject in hideOnPauseObjects)
                pauseObject.SetActive(!isPause);
        }
    }
}
