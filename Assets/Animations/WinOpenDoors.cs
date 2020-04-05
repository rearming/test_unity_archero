using System.Collections;
using System.Collections.Generic;
using GenericScripts;
using UnityEngine;
using EventType = GenericScripts.EventType;

public class WinOpenDoors : MonoBehaviour
{
    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
        EventManager.Instance.AddListener(EventType.Win, OpenDoors);
    }

    private void OpenDoors(EventType eventType, Component sender, object param = null)
    {
        _animator.SetTrigger("GameWon");
    }
}
