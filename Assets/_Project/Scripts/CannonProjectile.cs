using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CannonProjectile : MonoBehaviour
{
    [SerializeField] private TrailRenderer _trailRenderer;
    [SerializeField] private float _projectileSpeed;
    [SerializeField] private float _lifetime;

    private ProjectilePool _projectilePool;
    private Rigidbody2D _rb2d;
    private int _damage;

    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();

        Invoke(nameof(Destroy), _lifetime);
    }

    public void Initialize(int damage, ProjectilePool projectilePool)
    {
        _damage = damage;
        _projectilePool = projectilePool;

        _rb2d.velocity = Vector2.zero;
        _rb2d.AddForce(-transform.up * _projectileSpeed, ForceMode2D.Impulse);
        _trailRenderer.Clear();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent(out IDamageable damageable))
        {
            return;
        }

        damageable.TakeDamage(_damage);
        Destroy();
    }

    private void Destroy()
    {
        _projectilePool.ReturnProjectile(this);
    }
}
