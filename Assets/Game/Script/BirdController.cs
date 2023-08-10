using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    [SerializeField] public Rigidbody2D _rigi { get; private set; }
    [SerializeField] float _force;
    [SerializeField] float _yBound;
    [SerializeField] GameManager _gameManager;
    [SerializeField] UIManager _uiManager;
    [SerializeField] float _time;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _clipFlap;
    [SerializeField] AudioClip _clipDie;

    private float _elapsedTime;
    // Start is called before the first frame update
    void Start()
    {
        this._rigi = GetComponent<Rigidbody2D>();
        this._gameManager = GameManager.Instant;
        this._uiManager = UIManager.Instant;
        this._audioSource = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameManager._gameState != GameState.Play) {
            _rigi.velocity = Vector3.zero;
            return; 
        }
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime > _time)
        {
            if ((Input.GetMouseButtonDown(0) || Input.GetKey(KeyCode.Space)) && _rigi.position.y < _yBound)
            {
                Flap();
                _elapsedTime = 0f;
            }
        }


    }

    void Flap()
    {
        _rigi.velocity = Vector3.zero;
        _rigi.AddForce(Vector2.up * _force);
        _audioSource.PlayOneShot(_clipFlap);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("BG"))
        {
            _gameManager._gameState = GameState.Over;
            KeyValuePair<string, JSONNode> tmpItem = DataManager.Instant.GetHighScoreItem();
            string highScore = $"{tmpItem.Key}: {tmpItem.Value.AsInt:00#}";
            if (_gameManager.getScore() >= tmpItem.Value.AsInt)
            {
                _uiManager.NewHighScore();
            }
            else
            {
                _uiManager.GameOver(highScore);
            }
            _audioSource.PlayOneShot(_clipDie);
        }
    }

}
