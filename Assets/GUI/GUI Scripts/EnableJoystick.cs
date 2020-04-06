using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using GenericScripts;
using UnityEngine;
using UnityEngine.EventSystems;
using EventType = GenericScripts.EventType;

public class EnableJoystick : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
	public void OnPointerDown(PointerEventData eventData)
	{
		EventManager.Instance.PostNostrification(EventType.JoystickOn, this);
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		EventManager.Instance.PostNostrification(EventType.JoystickOff, this);
	}
}
