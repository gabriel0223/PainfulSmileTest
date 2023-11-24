using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Serialization;

public class ShipShooting : MonoBehaviour
{
    public event Action OnPlayerFire;
    public event Action OnPlayerTripleFire;

    [SerializeField] private float _timeBetweenShots;
    [SerializeField] private CannonProjectile _projectilePrefab;
    [SerializeField] private CannonController _frontalCannon;
    [SerializeField] private CannonController[] _sideCannons;
    [SerializeField] private InputManager _inputManager;

    private ObjectPool<CannonProjectile> _projectilePool;
    private bool _canFrontFire = true;
    private bool _canTripleFire = true;

    private void OnEnable()
    {
        _inputManager.OnFire += HandleShipFire;
        _inputManager.OnTripleFire += HandleTripleFire;
    }

    private void OnDisable()
    {
        _inputManager.OnFire -= HandleShipFire;
        _inputManager.OnTripleFire -= HandleTripleFire;
    }

    private void Start()
    {
        _projectilePool = new ObjectPool<CannonProjectile>(() => Instantiate(_projectilePrefab), 
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
        if (!_canFrontFire)
        {
            return;
        }

        CannonProjectile projectile = _projectilePool.Get();
        _frontalCannon.Fire(projectile);

        OnPlayerFire?.Invoke();

        StartCoroutine(FireCooldownCoroutine());
    }

    IEnumerator FireCooldownCoroutine()
    {
        _canFrontFire = false;

        yield return new WaitForSeconds(_timeBetweenShots);

        _canFrontFire = true;
    }

    private void HandleTripleFire()
    {
        if (!_canTripleFire)
        {
            return;
        }

        foreach (CannonController cannon in _sideCannons)
        {
            CannonProjectile projectile = _projectilePool.Get();
            cannon.Fire(projectile);
        }

        OnPlayerTripleFire?.Invoke();

        StartCoroutine(TripleFireCooldownCoroutine());
    }

    IEnumerator TripleFireCooldownCoroutine()
    {
        _canTripleFire = false;

        yield return new WaitForSeconds(_timeBetweenShots);

        _canTripleFire = true;
    }
}
