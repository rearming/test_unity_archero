using System;
using System.Collections;
using System.Collections.Generic;
using Attack;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

public class ShootingWeapon : MonoBehaviour, IShooter
{
    private ObjectPool _objectPool;
    
    [SerializeField] protected float weaponDamage;
    [SerializeField] protected float projectileSpeed;
    [SerializeField] protected GameObject projectilePrefab;

    private Transform _weaponTransform;
    void Start()
    {
        _objectPool = GameObject.FindGameObjectWithTag("ObjectPool").GetComponent<ObjectPool>();
        _weaponTransform = GetComponent<Transform>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown((int)MouseButton.LeftMouse))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                var targetDir = hit.point - _weaponTransform.position;
                targetDir.y = 0;
                Shoot(targetDir);
            }
        }
    }

    public void Shoot(Vector3 targetDir)
    {
        var projectileObject = _objectPool.GetGameObjectFromPool(projectilePrefab);
        var projectileComponent = projectileObject.GetComponent<GenericProjectile>();
        
        projectileObject.transform.position = _weaponTransform.position;
        projectileComponent.UpdateProjectileParams(weaponDamage, projectileSpeed);
        projectileComponent.StartFlight(targetDir);
    }
}
