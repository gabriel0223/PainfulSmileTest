using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CanonProjectile : MonoBehaviour
{
    [SerializeField] private float _projectileSpeed;

    private Rigidbody2D _rb2d;

    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    public void Fire()
    {
        _rb2d.velocity = Vector2.zero;
        _rb2d.AddForce(-transform.up * _projectileSpeed, ForceMode2D.Impulse);
    }
}
