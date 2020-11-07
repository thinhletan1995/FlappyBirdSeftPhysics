using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonComponent<GameManager>
{
    public AudioManager audioManager;
    
    [SerializeField] private BlockSpawner blockSpawner;
    [SerializeField] private UIManager uiManager;
    
    public int Point
    {
        get => point;
        set
        {
            point = value;
            uiManager.UpdateCurrentUI(new UIPlaying.Data(point));
        }
    }
    private int point = 0;
    public GameState State
    {
        get => state;
        set
        {
            state = value;

            switch (state)
            {
                case GameState.Waiting:
                    blockSpawner.Enable(false);
                    uiManager.ShowUIGroup<UIWaiting>();
                    break;
                
                case GameState.Playing:
                    blockSpawner.Enable(true);
                    uiManager.ShowUIGroup<UIPlaying>(new UIPlaying.Data(Point));
                    break;
                
                case GameState.Dead:
                    blockSpawner.Enable(false);
                    
                    int bestScore = PlayerPrefs.GetInt(Key.SCORE, 0);
                    bool newScoreRecord = false;
                    if (Point > bestScore)
                    {
                        newScoreRecord = true;
                        PlayerPrefs.SetInt(Key.SCORE, Point);
                    }
                    
                    uiManager.ShowUIGroup<UIGameover>(new UIGameover.Data(Point, newScoreRecord));
                    break;
            }
        }
    }
    private GameState state = GameState.Waiting;

    public void AddPoint(int value)
    {
        Point += value;
    }

    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Start()
    {
        State = GameState.Waiting;
    }

    public enum GameState
    {
        Waiting,
        Playing,
        Dead
    }
}
