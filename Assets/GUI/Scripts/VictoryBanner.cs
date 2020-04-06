using GenericScripts;
using UnityEngine;
using EventType = GenericScripts.EventType;

namespace GUI.Scripts
{
	public class VictoryBanner : MonoBehaviour
	{
		[SerializeField] private GameObject victoryBanner;
		private void Start()
		{
			EventManager.Instance.AddListener(EventType.Win, (type, sender, o) => {victoryBanner.SetActive(true); });
		}
	}
}
