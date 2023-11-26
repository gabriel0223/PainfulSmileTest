using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HudView : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _timerText;

    private void OnEnable()
    {
        _gameManager.OnScoreUpdate += HandleScoreUpdated;
    }

    private void OnDisable()
    {
        _gameManager.OnScoreUpdate -= HandleScoreUpdated;
    }

    private void Update()
    {
        DisplayTimer();
    }

    private void DisplayTimer()
    {
        int timerInMinutes = (int)_gameManager.Timer / 60;
        int timerInSeconds = (int)_gameManager.Timer % 60;

        _timerText.SetText($"{timerInMinutes:0}:{timerInSeconds:00}");
    }

    private void HandleScoreUpdated(int updatedScore)
    {
        _scoreText.SetText(updatedScore.ToString());
    }
}
