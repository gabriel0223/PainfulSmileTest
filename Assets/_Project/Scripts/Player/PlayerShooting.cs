using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputManager), typeof(ShipShooting))]
public class PlayerShooting : MonoBehaviour
{
    public event Action OnPlayerFire;
    public event Action OnPlayerTripleFire;

    private InputManager _inputManager;
    private ShipShooting _shipShooting;

    private void Awake()
    {
        _inputManager = GetComponent<InputManager>();
        _shipShooting = GetComponent<ShipShooting>();
    }

    private void OnEnable()
    {
        _inputManager.OnFire += HandleShipFirePressed;
        _inputManager.OnTripleFire += HandleTripleShipFirePressed;
    }

    private void OnDisable()
    {
        _inputManager.OnFire -= HandleShipFirePressed;
        _inputManager.OnTripleFire -= HandleTripleShipFirePressed;
    }

    private void HandleShipFirePressed()
    {
        if (!_shipShooting.CanFrontFire)
        {
            return;
        }

        _shipShooting.FireFrontCannon();
        OnPlayerFire?.Invoke();
    }

    private void HandleTripleShipFirePressed()
    {
        if (!_shipShooting.CanTripleFire)
        {
            return;
        }

        _shipShooting.FireTripleCannons();
        OnPlayerTripleFire?.Invoke();
    }
}
