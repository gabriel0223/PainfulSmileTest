using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CannonProjectile : MonoBehaviour
{
    [SerializeField] private float _projectileSpeed;

    private Rigidbody2D _rb2d;
    private int _damage;

    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    public void Initialize(int damage)
    {
        _damage = damage;

        _rb2d.velocity = Vector2.zero;
        _rb2d.AddForce(-transform.up * _projectileSpeed, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent(out IDamageable damageable))
        {
            return;
        }

        damageable.TakeDamage(_damage);
        Destroy(gameObject);
    }
}
