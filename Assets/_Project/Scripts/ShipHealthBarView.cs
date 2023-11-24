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
        _shipHealth.OnHealthUpdated += UpdateHealthBar;
    }

    private void OnDisable()
    {
        _shipHealth.OnHealthUpdated -= UpdateHealthBar;
    }

    private void Start()
    {
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        float healthPercentage = (float)_shipHealth.CurrentHealth / _shipHealth.MaxHealth;

        _healthText.SetText($"{_shipHealth.CurrentHealth}/{_shipHealth.MaxHealth}");
        _healthBar.DOSizeDelta(
            new Vector2(_initialHealthBarWidth * healthPercentage, _healthBar.sizeDelta.y), 0.25f);
    }
}
