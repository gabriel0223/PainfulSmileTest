using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ShipHealth : MonoBehaviour, IDamageable
{
    public event Action OnHealthUpdated; 

    [SerializeField] private int _maxHealth;

    private bool _isDead;

    public int MaxHealth => _maxHealth;
    public int CurrentHealth { get; private set; }

    private void Awake()
    {
        CurrentHealth = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (_isDead)
        {
            return;
        }

        CurrentHealth -= damage;

        if (CurrentHealth <= 0)
        {
            Die();
        }

        OnHealthUpdated?.Invoke();
    }

    private void Die()
    {
        _isDead = true;
    }
}
