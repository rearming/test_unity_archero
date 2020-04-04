using System.Collections;
using System.Collections.Generic;
using GenericScripts;
using UnityEngine;

public class WinOpenDoors : MonoBehaviour
{
    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
        EventManager.Instance.AddListener(EVENT_TYPE.Win, OpenDoors);
    }

    private void OpenDoors(EVENT_TYPE eventType, Component sender, object param = null)
    {
        _animator.SetTrigger("GameWon");
    }
}
