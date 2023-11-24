using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CannonController : MonoBehaviour
{
    [SerializeField] private ParticleSystem _cannonParticles;
    [SerializeField] private int _damage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fire(CannonProjectile projectile)
    {
        projectile.transform.position = transform.position;
        projectile.transform.rotation = transform.rotation;

        _cannonParticles.Play();
        projectile.Initialize(_damage);
    }
}
