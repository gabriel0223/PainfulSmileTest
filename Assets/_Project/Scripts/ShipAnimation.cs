using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ShipHealth))]
public class ShipAnimation : MonoBehaviour
{
    private const float WhiteOverlayDuration = 0.1f;

    [Tooltip("Ship sprites from less damaged to most damaged.")]
    [SerializeField] private Sprite[] _shipDamageSprites;
    [SerializeField] private Sprite _shipDestroyedSprite;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private ParticleSystem _shipHitEffect;
    [SerializeField] private GameObject _whiteOverlay;

    private ShipHealth _shipHealth;

    private void Awake()
    {
        _shipHealth = GetComponent<ShipHealth>();
    }

    private void OnEnable()
    {
        _shipHealth.OnTakeDamage += HandleTakeDamage;
    }

    private void OnDisable()
    {
        _shipHealth.OnTakeDamage -= HandleTakeDamage;
    }

    private void HandleTakeDamage()
    {
        StartCoroutine(DamageAnimationCoroutine());
    }

    private IEnumerator DamageAnimationCoroutine()
    {
        _shipHitEffect.Play();
        _whiteOverlay.SetActive(true);

        yield return new WaitForSeconds(WhiteOverlayDuration);

        _whiteOverlay.SetActive(false);

        if (_shipHealth.IsDead)
        {
            _spriteRenderer.sprite = _shipDestroyedSprite;
        }
        else
        {
            int spriteIndex = Mathf.CeilToInt(_shipHealth.CurrentHealth / (_shipHealth.MaxHealth / (float)_shipDamageSprites.Length));

            _spriteRenderer.sprite = _shipDamageSprites[^spriteIndex];
        }
    }
}
