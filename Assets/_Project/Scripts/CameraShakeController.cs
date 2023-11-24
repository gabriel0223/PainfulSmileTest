using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineImpulseSource))]
public class CameraShakeController : MonoBehaviour
{
    [SerializeField] private ShipShooting _playerShooting;
    [SerializeField] private float _playerFireShakeForce;
    [SerializeField] private float _playerTripleFireShakeForce;
    [SerializeField] private float _playerDamageShakeForce;
    [SerializeField] private float _enemyDeathShakeForce;

    private CinemachineImpulseSource _cinemachineImpulseSource;

    private void Awake()
    {
        _cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
    }

    private void OnEnable()
    {
        _playerShooting.OnPlayerFire += HandlePlayerFire;
        _playerShooting.OnPlayerTripleFire += HandlePlayerTripleFire;

    }

    private void OnDisable()
    {
        _playerShooting.OnPlayerFire -= HandlePlayerFire;
        _playerShooting.OnPlayerTripleFire -= HandlePlayerTripleFire;
    }

    private void HandlePlayerFire()
    {
        ExecuteCameraShake(_playerFireShakeForce);
    }

    private void HandlePlayerTripleFire()
    {
        ExecuteCameraShake(_playerTripleFireShakeForce);
    }

    private void ExecuteCameraShake(float force)
    {
        _cinemachineImpulseSource.GenerateImpulseWithForce(force);
    }
}
