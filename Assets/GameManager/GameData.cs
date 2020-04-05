using GenericScripts;
using UnityEngine;
using EventType = GenericScripts.EventType;

namespace GameManager
{
    public class GameData : MonoBehaviour
    {
        public int PlayerMoney { get; private set; }

        public void SendReward(int reward)
        {
            PlayerMoney += reward;
            EventManager.Instance.PostNostrification(EventType.SendReward, this);
        }
    }
}
