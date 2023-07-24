using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Win,
    Play,
    Over,
    Pause
}

public class GameManager : SingleTon<GameManager>
{
    [SerializeField] BirdController _bird;

    public BirdController bird => _bird;
    [SerializeField] MarioForceGame _mario;
    public MarioForceGame mario => _mario;
    [SerializeField] private int _score = 0;
    public GameState _gameState = GameState.Play;
    // Start is called before the first frame update

    public int getScore()
    {
        return _score;
    }

    public void setScore(int score)
    {
        this._score = score;
    }
}
