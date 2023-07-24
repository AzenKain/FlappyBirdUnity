using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private float _time;
    [SerializeField] private float _yClamp;
    [SerializeField] ObjectPooling _pool;
    [SerializeField] GameManager _gameManager;

    private float _elapsedTime;

    private void Start()
    {
        this._pool = ObjectPooling.Instant;
        this._gameManager = GameManager.Instant;
    }

    private void Update()
    {
        if (_gameManager._gameState != GameState.Play) { return; }

        _elapsedTime += Time.deltaTime;

        if (_elapsedTime > _time)
        {
            SpawnObject();

            _elapsedTime = 0f;
        }
    }

    private void SpawnObject()
    {
        float offsetY = UnityEngine.Random.Range(-_yClamp, _yClamp);

        GameObject _itemSpam = _pool.getObj(_prefab);
        Vector2 pos = new Vector2(this.transform.position.x, this.transform.position.y + offsetY);
        _itemSpam.transform.position = pos;
        _itemSpam.GetComponent<MovingObject>().Init();
        _itemSpam.SetActive(true);
    }

}
