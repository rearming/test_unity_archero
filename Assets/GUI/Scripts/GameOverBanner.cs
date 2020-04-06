using GenericScripts;
using UnityEngine;
using EventType = GenericScripts.EventType;

namespace GUI.Scripts
{
    public class GameOverBanner : MonoBehaviour
    {
        [SerializeField] private GameObject gameOverBanner;
        private void Start()
        {
            EventManager.Instance.AddListener(EventType.Loose, (type, sender, o) => {gameOverBanner.SetActive(true); });
        }
    }
}
