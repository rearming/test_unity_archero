using GenericScripts;
using UnityEngine;
using UnityEngine.EventSystems;
using EventType = GenericScripts.EventType;

namespace GUI.Scripts
{
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
}
