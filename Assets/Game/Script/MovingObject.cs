using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    [SerializeField] protected float _speed;
    [SerializeField] protected float _xBound;
    protected float tempSpeed;
    [SerializeField] GameManager _gameManager;
    // Start is called before the first frame update

    public void Init()
    {
        this.tempSpeed = _speed;
    }
    void Start()
    {
        this.tempSpeed = _speed;
        this._gameManager = GameManager.Instant;
    }

    // Update is called once per frame
    void Update()
    {

        if (_gameManager._gameState != GameState.Play)
        {
            this.transform.Translate(Vector3.zero);
            return;
        }
        Moving();
    }

    void Moving()
    {
        this.transform.Translate(Vector3.left * tempSpeed * Time.deltaTime);

        if (this.transform.position.x < _xBound)
        {
            this.gameObject.SetActive(false);
            this.tempSpeed = 0;
        }
    }
}
