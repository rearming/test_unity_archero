using System;
using System.Collections;
using System.Collections.Generic;
using GenericScripts;
using Player;
using UnityEngine;
using UnityEngine.UI;
using EventType = GenericScripts.EventType;

public class HealthBar : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] protected RectTransform fillingTransform;
    [SerializeField] protected RectTransform rightCorner;

    private PlayerCreature _playerCreature;
    private float _stepSize;
    private void Start()
    {
        _playerCreature = FindObjectOfType<PlayerCreature>();
        _stepSize = fillingTransform.sizeDelta.x / _playerCreature.Health;
        EventManager.Instance.AddListener(EventType.PlayerHit, (type, sender, param) =>
        {
            UpdateHealthBar(_stepSize * _playerCreature.DeltaHealth);
        });
    }

    private void UpdateHealthBar(float updateSize)
    {
        var sizeDelta = fillingTransform.sizeDelta;
        var fillingPosition = fillingTransform.anchoredPosition;
        var cornerPosition = rightCorner.anchoredPosition;
            
        sizeDelta.x += updateSize;
        fillingPosition.x += updateSize / 2;

        fillingTransform.sizeDelta = sizeDelta;
        fillingTransform.anchoredPosition = fillingPosition;
        cornerPosition.x = fillingTransform.rect.xMax;
        rightCorner.anchoredPosition = cornerPosition;
    }
    
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
            UpdateHealthBar(-speed);
        if (Input.GetKey(KeyCode.RightArrow))
            UpdateHealthBar(speed);
    }
}
