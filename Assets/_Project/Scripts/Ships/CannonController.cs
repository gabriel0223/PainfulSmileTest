using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CannonController : MonoBehaviour
{
    [SerializeField] private ParticleSystem _cannonParticles;
    [SerializeField] private int _damage;

    public void Fire(CannonProjectile projectile, ProjectilePool projectilePool)
    {
        projectile.transform.position = transform.position;
        projectile.transform.rotation = transform.rotation;

        _cannonParticles.Play();
        projectile.Initialize(_damage, projectilePool);
    }
}
