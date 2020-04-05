using System;
using System.Collections;
using System.Collections.Generic;
using GenericScripts;
using UnityEngine;
using EventType = GenericScripts.EventType;

public class VictoryBanner : MonoBehaviour
{
	[SerializeField] private GameObject victoryBanner;
	private void Start()
	{
		EventManager.Instance.AddListener(EventType.Win, (type, sender, o) => {victoryBanner.SetActive(true); });
	}
}
