using UnityEngine;

namespace GameManager
{
    public class GameData : MonoBehaviour
    {
        private int _playerMoney;

        public void SendReward(int reward)
        {
            _playerMoney += reward;
            Debug.Log($"Player owns {_playerMoney.ToString()} now");
        }
    }
}
