using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Serialization;

public class ShipShooting : MonoBehaviour
{
    [SerializeField] private float _timeBetweenShots;
    [SerializeField] private CannonController _frontalCannon;
    [SerializeField] private CannonController[] _sideCannons;

    private ProjectilePool _projectilePool;

    public bool CanFrontFire { get; private set; }
    public bool CanTripleFire { get; private set; }

    private void Start()
    {
        CanFrontFire = true;
        CanTripleFire = true;

        _projectilePool = FindObjectOfType<ProjectilePool>();
    }

    public void FireFrontCannon()
    {
        if (!CanFrontFire)
        {
            return;
        }

        CannonProjectile projectile = _projectilePool.GetProjectile();
        _frontalCannon.Fire(projectile, _projectilePool);

        StartCoroutine(FrontCannonCooldownCoroutine());
    }

    IEnumerator FrontCannonCooldownCoroutine()
    {
        CanFrontFire = false;

        yield return new WaitForSeconds(_timeBetweenShots);

        CanFrontFire = true;
    }

    public void FireTripleCannons()
    {
        if (!CanTripleFire)
        {
            return;
        }

        foreach (CannonController cannon in _sideCannons)
        {
            CannonProjectile projectile = _projectilePool.GetProjectile();
            cannon.Fire(projectile, _projectilePool);
        }

        StartCoroutine(TripleCannonsCooldownCoroutine());
    }

    IEnumerator TripleCannonsCooldownCoroutine()
    {
        CanTripleFire = false;

        yield return new WaitForSeconds(_timeBetweenShots);

        CanTripleFire = true;
    }
}
