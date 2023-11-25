using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(ShipShooting))]
public class AIShipShooting : MonoBehaviour
{
    [SerializeField] private float _maxDistanceToAttack;
    [SerializeField] private float _minTimeBetweenAttacks;
    [SerializeField] private float _maxTimeBetweenAttacks;

    private ShipHealth _shipHealth;
    private ShipShooting _shipShooting;
    private Transform _player;

    private void Awake()
    {
        _shipShooting = GetComponent<ShipShooting>();
        _shipHealth = GetComponent<ShipHealth>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<InputManager>().transform;

        StartCoroutine(AIAttackLoop());
    }

    IEnumerator AIAttackLoop()
    {
        while (!_shipHealth.IsDead)
        {
            if (Vector3.Distance(transform.position, _player.position) > _maxDistanceToAttack)
            {
                yield return null;
                continue;
            }

            Vector3 playerDirection = (_player.position - transform.position).normalized;
            float dotProduct = Vector3.Dot(-transform.up, playerDirection);

            switch (dotProduct)
            {
                case > 0.8f:
                    _shipShooting.FireFrontCannon();
                    break;
                case > -0.4f and < 0.4f:
                    _shipShooting.FireTripleCannons();
                    break;
            }

            yield return new WaitForSeconds(Random.Range(_minTimeBetweenAttacks, _maxTimeBetweenAttacks));
        }
    }
}
