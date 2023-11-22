using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Serialization;

public class ShipShooting : MonoBehaviour
{
    [SerializeField] private float _timeBetweenShots;
    [SerializeField] private CanonProjectile _projectilePrefab;
    [SerializeField] private Transform _frontalShotFirePoint;
    [SerializeField] private InputManager _inputManager;

    private ObjectPool<CanonProjectile> _projectilePool;
    private bool _canFire = true;

    private void OnEnable()
    {
        _inputManager.OnFire += HandleShipFire;
    }

    private void OnDisable()
    {
        _inputManager.OnFire -= HandleShipFire;
    }

    private void Start()
    {
        _projectilePool = new ObjectPool<CanonProjectile>(() => Instantiate(_projectilePrefab), 
            projectile =>
        {
            projectile.gameObject.SetActive(true);
        }, projectile =>
        {
            projectile.gameObject.SetActive(false);
        }, projectile =>
        {
            Destroy(projectile.gameObject);
        }, false, 20, 50);
    }

    private void HandleShipFire()
    {
        if (!_canFire)
        {
            return;
        }

        CanonProjectile projectile = _projectilePool.Get();
        projectile.transform.position = _frontalShotFirePoint.position;
        projectile.transform.rotation = _frontalShotFirePoint.rotation;
        projectile.Fire();

        StartCoroutine(FireCooldownCoroutine());
    }

    IEnumerator FireCooldownCoroutine()
    {
        _canFire = false;

        yield return new WaitForSeconds(_timeBetweenShots);

        _canFire = true;
    }
}
