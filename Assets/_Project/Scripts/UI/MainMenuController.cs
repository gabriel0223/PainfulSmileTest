using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject _optionsPanel;
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _optionsButton;

    private void OnEnable()
    {
        _playButton.onClick.AddListener(StartGame);
        _optionsButton.onClick.AddListener(OpenOptionsMenu);
    }

    private void OnDisable()
    {
        _playButton.onClick.RemoveAllListeners();
        _optionsButton.onClick.RemoveAllListeners();
    }

    private void StartGame()
    {
        SceneManager.LoadScene((int)SceneIndex.Game);
    }

    private void OpenOptionsMenu()
    {
        _optionsPanel.SetActive(true);
    }
}
