using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ShipHealth : MonoBehaviour, IDamageable
{
    public event Action OnTakeDamage;
    public event Action OnDie;

    [SerializeField] private int _maxHealth;

    public int MaxHealth => _maxHealth;
    public int CurrentHealth { get; private set; }
    public float HealthPercentage => (float)CurrentHealth / MaxHealth;
    public bool IsDead { get; private set; }

    private void Awake()
    {
        CurrentHealth = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (IsDead)
        {
            return;
        }

        CurrentHealth -= damage;

        if (CurrentHealth <= 0)
        {
            Die();
        }

        OnTakeDamage?.Invoke();
    }

    private void Die()
    {
        IsDead = true;

        OnDie?.Invoke();
    }
}
