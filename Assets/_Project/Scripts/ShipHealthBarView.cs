using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShipHealthBarView : MonoBehaviour
{
    [SerializeField] private ShipHealth _shipHealth;
    [SerializeField] private RectTransform _healthBar;
    [SerializeField] private TMP_Text _healthText;

    private float _initialHealthBarWidth;

    private void Awake()
    {
        _initialHealthBarWidth = _healthBar.rect.width;
    }

    private void OnEnable()
    {
        _shipHealth.OnTakeDamage += UpdateHealthBar;
        _shipHealth.OnDie += HideHealthBar;
    }

    private void OnDisable()
    {
        _shipHealth.OnTakeDamage -= UpdateHealthBar;
        _shipHealth.OnDie -= HideHealthBar;
    }

    private void Start()
    {
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        _healthText.SetText($"{_shipHealth.CurrentHealth}/{_shipHealth.MaxHealth}");
        _healthBar.DOSizeDelta(
            new Vector2(_initialHealthBarWidth * _shipHealth.HealthPercentage, _healthBar.sizeDelta.y), 0.25f);
    }

    private void HideHealthBar()
    {
        Sequence hideSequence = DOTween.Sequence();

        hideSequence.AppendInterval(0.5f);
        hideSequence.Append(transform.DOScale(Vector2.zero, 0.25f));
        hideSequence.AppendCallback(() => Destroy(gameObject));
    }
}
