using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        private Transform _transform;

        private Vector3 _currMovementDir;
        private Vector3 _prevMovementDir;

        private float _rotationLerpValue;
        private bool _rotationComplete;

        private PlayerData _playerData;

        void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _transform = transform;
            _playerData = GetComponent<PlayerData>();
        }
    
        void Update()
        {
            UpdateMovementDir();
            UpdateMovementRotation();
            UpdatePlayerState();
            _rigidbody.velocity = _currMovementDir * +_playerData.moveSpeed;
        }

        void UpdatePlayerState()
        {
            if (_currMovementDir != Vector3.zero)
            {
                _playerData.state = PlayerState.Moving;
                _rotationComplete = false;
            }
            else if (_rotationComplete)
                _playerData.state = PlayerState.Idle;
        }
    
        void UpdateMovementRotation()
        {
            if (_rotationComplete)
                return;
            if (_currMovementDir != Vector3.zero)
                _prevMovementDir = _currMovementDir;
        
            _transform.localEulerAngles =
                _playerData.rotator.SmoothLookAt(_transform.localEulerAngles.y, _prevMovementDir, ref _rotationLerpValue);

            if (_rotationLerpValue > 1f)
            {
                _rotationComplete = true;
                _rotationLerpValue = 0;
            }
        }
    
        void UpdateMovementDir()
        {
            _currMovementDir.x = Input.GetAxis("Horizontal");
            _currMovementDir.z = Input.GetAxis("Vertical");
        }
    }
}
