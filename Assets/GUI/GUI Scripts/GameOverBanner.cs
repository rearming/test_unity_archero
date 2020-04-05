using System.Collections;
using System.Collections.Generic;
using GenericScripts;
using UnityEngine;
using EventType = GenericScripts.EventType;

public class GameOverBanner : MonoBehaviour
{
    [SerializeField] private GameObject gameOverBanner;
    private void Start()
    {
        EventManager.Instance.AddListener(EventType.Loose, (type, sender, o) => {gameOverBanner.SetActive(true); });
    }
}
