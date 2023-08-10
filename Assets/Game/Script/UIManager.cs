using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using SimpleJSON;

public class UIManager : SingleTon<UIManager>
{
    [SerializeField] TMP_Text _scoreText;
    [SerializeField] TMP_Text _highScoreText;
    [SerializeField] TMP_InputField _playerHighScoreField;
    [SerializeField] Button _pauseBtn;
    [SerializeField] Button _resumeBtn;
    [SerializeField] Image _gameOver;
    [SerializeField] GameManager _gameManager;
    // Start is called before the first frame update
    void Start()
    {
        this._gameManager = GameManager.Instant;
        this._scoreText = this.GetComponentInChildren<TMP_Text>();
        this._resumeBtn.onClick.AddListener(clickResumeGame);
        this._pauseBtn.onClick.AddListener(clickPauseGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void NewHighScore()
    {
        _highScoreText.text = "New High Score!";
        _highScoreText.gameObject.SetActive(true);
        _resumeBtn.gameObject.SetActive(true);
        _playerHighScoreField.gameObject.SetActive(true);
        DataManager.Instant.updateHighScore(_playerHighScoreField.text, _gameManager.getScore());
        _resumeBtn.onClick.AddListener(clickGameOver);
    }
    public void GameOver(string highScore)
    {

        _highScoreText.text = highScore;
        _highScoreText.gameObject.SetActive(true);
        _resumeBtn.gameObject.SetActive(true);
        _gameOver.gameObject.SetActive(true);
        _resumeBtn.onClick.RemoveAllListeners();
        _resumeBtn.onClick.AddListener(clickGameOver);
    }

    public void clickGameOver()
    {
        SceneManager.LoadScene(1);
    }
    public void clickResumeGame()
    {
        _resumeBtn.gameObject.SetActive(false);
        _pauseBtn.interactable = true;
        _gameManager._gameState = GameState.Play;
        _gameManager.bird._rigi.gravityScale = 1;
    }
    public void clickPauseGame()
    {
        _gameManager._gameState = GameState.Pause;
        _gameManager.bird._rigi.gravityScale = 0;
        _pauseBtn.interactable = false;
        _resumeBtn.gameObject.SetActive(true);
    }
    public void setUIScore(int score)
    {
        _scoreText.text = $"{score:00#}";
    }
}
