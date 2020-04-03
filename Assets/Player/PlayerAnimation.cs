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
            _animator.SetBool("Moving", _playerData.state == PlayerState.Moving);
            if (_playerData.state == PlayerState.Shooting)
                _animator.SetTrigger("Shoot");
            if (_playerData.state == PlayerState.Dying)
                _animator.SetBool("IsDead", true);
        }
    }
}
