using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputManager), typeof(Rigidbody2D))]
public class PlayerShipMovement : ShipMovementBase
{
    [SerializeField] private float _shipSpeed;
    [SerializeField] private float _turnSpeed;

    private InputManager _inputManager;
    private Rigidbody2D _rb2d;

    private void Awake()
    {
        _inputManager = GetComponent<InputManager>();
        _rb2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (_inputManager.AccelerationInput != 0)
        {
            _rb2d.AddForce(-transform.up * (_shipSpeed * _inputManager.AccelerationInput), ForceMode2D.Force);
        }

        if (_inputManager.TurnInput != 0)
        {
            _rb2d.AddTorque(-_turnSpeed * _inputManager.TurnInput, ForceMode2D.Force);
        }
    }

    public override Vector2 CurrentVelocity()
    {
        return _rb2d.velocity;
    }
}
