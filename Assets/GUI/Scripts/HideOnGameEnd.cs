using GenericScripts;
using UnityEngine;
using EventType = GenericScripts.EventType;

namespace GUI.Scripts
{
    public class HideOnGameEnd : MonoBehaviour
    {
        void Start()
        {
            EventManager.Instance.AddListener(EventType.Win, (type, sender, o) => gameObject.SetActive(false));
            EventManager.Instance.AddListener(EventType.Loose, (type, sender, o) => gameObject.SetActive(false));
        }
    }
}
