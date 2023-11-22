using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private Rigidbody2D _rb2d;
    [Header("Settings")] 
    [SerializeField] private float _shipSpeed;
    [SerializeField] private float _turnSpeed;

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
}
