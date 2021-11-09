using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _pauseUI;
    [SerializeField] private GameObject _gameCanvas;
    [SerializeField] private GameObject _gameOverCanvas;
    [SerializeField] private GameObject _winGameCanvas;
    [SerializeField] private Text _musicToggleText;
    [SerializeField] private SceneLoader _sceneLoader;
    
    [SerializeField] private AudioSource _musicAudioSource;
    [SerializeField] private AudioSource _gameOverAudioSource;

    private DataSceneStorage _dataSceneStorage;

    private PlayerLife _player;
    private bool _musicOn = true;

    private void Start()
    {
        _player = FindObjectOfType<PlayerLife>();
        _dataSceneStorage = FindObjectOfType<DataSceneStorage>();

        _player.Died += GameOver;
        _dataSceneStorage.LastWaypointReached += OnLastPointReached;
    }

    private void OnDisable()
    {
        _player.Died -= GameOver;
        _dataSceneStorage.LastWaypointReached -= OnLastPointReached;
    }

    private void ClearUI()
    {
        _pauseUI.SetActive(false);
        _gameCanvas.SetActive(false);
        _gameOverCanvas.SetActive(false);
        _winGameCanvas.SetActive(false);
    }
    
    private void WinGame()
    {
        ClearUI();
        Time.timeScale = 0;

        _winGameCanvas.SetActive(true);
    }

    private void GameOver()
    {
        ClearUI();
        Time.timeScale = 0;
        _musicAudioSource.Stop();
        _gameOverAudioSource.Play();
        _gameOverCanvas.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        
        ClearUI();
        _gameCanvas.SetActive(true);
    }

    public void Restart()
    {
        ClearUI();
        _gameCanvas.SetActive(true);

        _sceneLoader.LoadGameScene();

        Time.timeScale = 1;
    }

    public void MainMenu()
    {
        _sceneLoader.LoadMenuScene();
        Time.timeScale = 1;
    }

    public void ToggleMusic()
    {
        if (_musicOn)
        {
            _musicAudioSource.Stop();
            _musicToggleText.text = "MUSIC:OFF";
        }
        else
        {
            _musicAudioSource.Play();
            _musicToggleText.text = "MUSIC:ON";

        }
        
        _musicOn = !_musicOn;
    }

    public void Pause()
    {
        Time.timeScale = 0;
        
        ClearUI();
        _pauseUI.SetActive(true);
    }

    
    private void OnLastPointReached()
    {
        WinGame();
    }
}
