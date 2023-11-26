using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(ShipHealth))]
public class AIShipMovement : ShipMovementBase
{
    [SerializeField] private float _turnSpeed;
    [Tooltip("The minimum distance required for the AI to start chasing the player")]
    [SerializeField] private float _minDistanceToChasePlayer;

    private NavMeshAgent _agent;
    private ShipHealth _shipHealth;
    private Transform _player;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _shipHealth = GetComponent<ShipHealth>();

        _agent.updateUpAxis = false;
        _agent.updateRotation = false;
    }

    private void OnEnable()
    {
        _shipHealth.OnDie += HandleShipDie;
    }

    private void OnDisable()
    {
        _shipHealth.OnDie -= HandleShipDie;
    }

    private void Start()
    {
        _player = FindObjectOfType<PlayerTag>().transform;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, _player.position) > _minDistanceToChasePlayer)
        {
            _agent.SetDestination(_player.position);
        }

        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, -_agent.velocity.normalized);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _turnSpeed * Time.deltaTime);
    }

    public override Vector2 CurrentVelocity()
    {
        return _agent.velocity;
    }

    private void HandleShipDie()
    {
        enabled = false;
    }
}
