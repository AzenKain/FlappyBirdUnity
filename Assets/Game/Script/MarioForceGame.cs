using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioForceGame : MonoBehaviour
{
    [SerializeField] GameManager _gameManager;
    [SerializeField] UIManager _uiManager;
    [SerializeField] Rigidbody2D _rigiMario;
    [SerializeField] float _speed;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _clipHit;
    [SerializeField] AudioClip _clipSwoosh;
    private bool _forceGame;
    // Start is called before the first frame update
    void Start()
    {
        this._rigiMario = GetComponent<Rigidbody2D>();
        this._gameManager = GameManager.Instant;
        this._uiManager = UIManager.Instant;
        this._audioSource = this.GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!_audioSource.isPlaying)
        {
            this._audioSource.PlayOneShot(_clipSwoosh);
        }
    }

    public void setForceGame(bool idx)
    {
        _forceGame = idx;
    }

    private void FixedUpdate()
    {
        if (this._forceGame == true)
        {
            forceGame();
        }
        if (this.transform.position.x < -10)
        {
            this.gameObject.SetActive(false);
        }
    }

    void forceGame()
    {
        this._rigiMario.velocity = Vector2.left * _speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bird"))
        {
            _audioSource.PlayOneShot(_clipHit);
            _gameManager._gameState = GameState.Over;
            KeyValuePair<string, JSONNode> tmpItem = DataManager.Instant.GetHighScoreItem();
            string highScore = $"{tmpItem.Key}: {tmpItem.Value.AsInt:00#}";
            if (_gameManager.getScore() > tmpItem.Value.AsInt)
            {
                _uiManager.NewHighScore();
            }
            else
            {
                _uiManager.GameOver(highScore);
            }
        }
    }

}
