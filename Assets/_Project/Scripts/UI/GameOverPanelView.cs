using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanelView : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private Button _playAgainButton;
    [SerializeField] private Button _mainMenunButton;

    private void OnEnable()
    {
        _gameManager.OnGameOver += DisplayGameOverPanel;
        _playAgainButton.onClick.AddListener(HandlePlayAgainClicked);
        _mainMenunButton.onClick.AddListener(HandleMainMenuClicked);
    }

    private void OnDisable()
    {
        _gameManager.OnGameOver -= DisplayGameOverPanel;
        _playAgainButton.onClick.RemoveAllListeners();
        _mainMenunButton.onClick.RemoveAllListeners();
    }

    private void HandlePlayAgainClicked()
    {
        _gameManager.RestartGame();
    }

    private void HandleMainMenuClicked()
    {
        _gameManager.GoBackToMainMenu();
    }

    private void DisplayGameOverPanel()
    {
        _scoreText.SetText($"Score: {_gameManager.Score.ToString()}");

        _gameOverPanel.SetActive(true);
    }
}
