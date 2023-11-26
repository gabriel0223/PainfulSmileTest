using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public event Action OnFire;
    public event Action OnTripleFire;

    private PlayerControls _playerControls;

    public float AccelerationInput { get; private set; }
    public float TurnInput { get; private set; }

    private void Awake()
    {
        _playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        _playerControls.Enable();

        _playerControls.Player.Fire.performed += ctx => OnFire?.Invoke();
        _playerControls.Player.TripleFire.performed += ctx => OnTripleFire?.Invoke();
    }

    private void OnDisable()
    {
        _playerControls.Disable();
    }

    private void Update()
    {
        AccelerationInput = _playerControls.Player.Accelerate.ReadValue<float>();
        TurnInput = _playerControls.Player.Turn.ReadValue<float>();
    }

    public void DisableInput()
    {
        _playerControls.Disable();
    }
}
