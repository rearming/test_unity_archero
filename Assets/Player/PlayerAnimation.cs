using GenericScripts;
using UnityEngine;
using UnityEngine.UIElements;

namespace Player
{
    public class PlayerAnimation : MonoBehaviour
    {
        private PlayerData _playerData;
        private SmoothRotator _smoothRotator;

        private Animator _animator;

        private Transform _transform;

        private float _rotationLerpValue;
        private bool _rotationCompleted;

        private Vector3 _enemyPos;
    
        void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
            _transform = transform;
            _playerData = GetComponent<PlayerData>();
            _smoothRotator = new SmoothRotator(0.1f, 4);
        }

        void Update()
        {
            if (_playerData.State == PlayerState.GetHit)
            {
                _playerData.State = _playerData.GetPrevState();
                _animator.SetTrigger("GetHit");
            }
            if (_playerData.State == PlayerState.Dead)
            {
                _animator.SetTrigger("Die");
                return;
            }
            _animator.SetBool("Moving", _playerData.State == PlayerState.Moving);
            if (_playerData.State == PlayerState.Shooting)
                _animator.SetTrigger("Shoot");
            
        }
    }
}
