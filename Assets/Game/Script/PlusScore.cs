using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlusScore : MonoBehaviour
{
    [SerializeField] GameManager _gameManager;
    [SerializeField] UIManager _uiManager;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _clipPoint;
    // Start is called before the first frame update
    void Start()
    {
        this._gameManager = GameManager.Instant;
        this._uiManager = UIManager.Instant;
        this._audioSource = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bird"))
        {
            _gameManager.setScore(_gameManager.getScore() + 1);
            _uiManager.setUIScore(_gameManager.getScore());
            this._audioSource.PlayOneShot(_clipPoint);
            if (_gameManager.getScore() == 95)
            {
                _gameManager.mario.gameObject.SetActive(true);
                _gameManager.mario.setForceGame(true);
            }
        }
    }
}
