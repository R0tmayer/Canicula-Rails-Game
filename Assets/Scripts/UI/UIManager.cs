using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Canvases")]
    [SerializeField] private GameObject _pauseUI;
    [SerializeField] private GameObject _gameCanvas;
    [SerializeField] private GameObject _gameOverCanvas;
    [SerializeField] private GameObject _winGameCanvas;
    [SerializeField] private Text _musicToggleText;
    [SerializeField] private SceneLoader _sceneLoader;
    [SerializeField] private StatsManager _statsManager;


    [Header("Victory Stats Texts")]
    [SerializeField] private Text _victoryHealthText;
    [SerializeField] private Text _victoryAccuracyText;
    [SerializeField] private Text _victoryCompletionText;
    [SerializeField] private Text _victoryFinalScoreText;
    [SerializeField] private Image _victoryMedal;
    
    [Header("GameOver Stats Texts")]
    [SerializeField] private Text _gameOverAccuracyText;
    [SerializeField] private Text _gameOverCompletionText;
    [SerializeField] private Text _gameOverFinalScoreText;
    [SerializeField] private Image _gameOverMedal;
    
    [Header("Music")]
    [SerializeField] private AudioSource _musicAudioSource;
    [SerializeField] private AudioSource _gameOverAudioSource;
    [SerializeField] private AudioSource _victoryMusic;    
    
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
        _musicAudioSource.Stop();
        _victoryMusic.Play();

        _statsManager.CalculateScores();
        _statsManager.CalculateStatsPercents();
        _statsManager.CalculateReachedMedal();

        _victoryHealthText.text = "Health: " + (int)_statsManager.HealthPercent + "%";
        _victoryAccuracyText.text = "Accuracy: " + (int)_statsManager.AccuracyPercent + "%";
        _victoryCompletionText.text = "Completion: " + (int)_statsManager.CompletionPercent + "%";
        _victoryFinalScoreText.text = "FinalScore: " + (int)_statsManager.FinalScores;
        _victoryMedal.sprite = _statsManager.ReachedMedal;
        
        _winGameCanvas.SetActive(true);
    }

    private void GameOver()
    {
        ClearUI();
        Time.timeScale = 0;
        _musicAudioSource.Stop();
        _gameOverAudioSource.Play();
        
        _statsManager.CalculateScores();
        _statsManager.CalculateStatsPercents();
        _statsManager.CalculateReachedMedal();

        _gameOverAccuracyText.text = "Accuracy: " + (int)_statsManager.AccuracyPercent + "%";
        _gameOverCompletionText.text = "Completion: " + (int)_statsManager.CompletionPercent + "%";
        _gameOverFinalScoreText.text = "FinalScore: " + (int)_statsManager.FinalScores;
        _gameOverMedal.sprite = _statsManager.ReachedMedal;

        
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
