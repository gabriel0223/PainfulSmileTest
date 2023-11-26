using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RamController : MonoBehaviour
{
    [SerializeField] private ShipHealth _shipHealth;
    [SerializeField] private ShipMovementBase _shipMovement;
    [Tooltip("How much damage is going to be caused in relation to the impact force")]
    [SerializeField] private AnimationCurve _damageOverImpactForce;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_shipHealth.IsDead)
        {
            return;
        }

        if (!other.TryGetComponent(out IDamageable damageable) || !other.TryGetComponent(out ShipMovementBase hitShip))
        {
            return;
        }

        float impactForce = (_shipMovement.CurrentVelocity() - hitShip.CurrentVelocity()).magnitude;

        if (impactForce < 1)
        {
            return;
        }

        int damageToBeCaused = (int)_damageOverImpactForce.Evaluate(impactForce);
        damageable.TakeDamage(damageToBeCaused);
    }
}
