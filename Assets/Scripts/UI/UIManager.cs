using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Canvases")]
    [SerializeField] private GameObject _pauseUI;
    [SerializeField] private GameObject _gameCanvasUI;
    [SerializeField] private GameObject _gameOverCanvas;
    [SerializeField] private Text _soundToggleText;
    
    [SerializeField] private AudioSource _musicAudioSource;
    [SerializeField] private AudioSource _gameOverAudioSource;

    private PlayerLife _player;
    private bool _musicOn = true;

    private void Start()
    {
        _player = FindObjectOfType<PlayerLife>();
        _player.Died += GameOver;

    }

    private void OnDisable()
    {
        _player.Died -= GameOver;
    }   

    private void ClearUI()
    {
        _pauseUI.SetActive(false);
        _gameCanvasUI.SetActive(false);
        _gameOverCanvas.SetActive(false);
    }
    
    public void Resume()
    {
        Time.timeScale = 1;
        
        ClearUI();
        _gameCanvasUI.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(StaticSceneNames.GAME_SCENE);
        Time.timeScale = 1;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(StaticSceneNames.MENU_SCENE);
        Time.timeScale = 1;
    }

    public void ToggleMusic()
    {
        if (_musicOn)
        {
            _musicAudioSource.Stop();
            _soundToggleText.text = "MUSIC:OFF";
        }
        else
        {
            _musicAudioSource.Play();
            _soundToggleText.text = "MUSIC:ON";

        }
        
        _musicOn = !_musicOn;
    }

    public void Pause()
    {
        Time.timeScale = 0;
        
        ClearUI();
        _pauseUI.SetActive(true);

    }

    private void GameOver()
    {
        ClearUI();
        // _musicAudioSource.Stop();
        _gameOverAudioSource.Play();
        _gameOverCanvas.SetActive(true);
    }
}
