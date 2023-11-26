using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class OptionsPanelView : MonoBehaviour
{
    [SerializeField] private Button _closeButton;
    [SerializeField] private Slider _gameSessionTimeSlider;
    [SerializeField] private Slider _enemySpawnTimeSlider;

    private void OnEnable()
    {
        _closeButton.onClick.AddListener(ClosePanel);
        _gameSessionTimeSlider.onValueChanged.AddListener(UpdateGameSessionTimeSetting);
        _enemySpawnTimeSlider.onValueChanged.AddListener(UpdateEnemySpawnTimeSetting);
    }

    private void OnDisable()
    {
        _closeButton.onClick.RemoveAllListeners();
        _gameSessionTimeSlider.onValueChanged.RemoveAllListeners();
        _enemySpawnTimeSlider.onValueChanged.RemoveAllListeners();
    }

    void Start()
    {
        _gameSessionTimeSlider.value = GameSettings.GameSessionTime;
        _enemySpawnTimeSlider.value = GameSettings.EnemySpawnTime;
    }

    private void ClosePanel()
    {
        gameObject.SetActive(false);
    }

    private void UpdateGameSessionTimeSetting(float sliderValue)
    {
        GameSettings.SetGameSessionTime(sliderValue);
    }

    private void UpdateEnemySpawnTimeSetting(float sliderValue)
    {
        GameSettings.SetEnemySpawnTime(sliderValue);
    }
}
