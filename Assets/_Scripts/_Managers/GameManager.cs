using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState gameState;
    public float timeRemaining = 10;

    [SerializeField] private Image timerBar;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ChangeGameState(GameState.GenerateGrid);
    }

    private void Update()
    {
        timerBar.rectTransform.sizeDelta = new Vector2(10*timeRemaining, 20);
        if (gameState == GameState.PlayerTurn)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                ChangeGameState(GameState.GameOver);
            }
        }
    }

    public void ChangeGameState(GameState state)
    {
        gameState = state;

        switch (state)
        {
            case GameState.GenerateGrid:
                GridManager.Instance.GenerateGrid();
                break;
            case GameState.PlayerTurn:
                UnitManager.Instance.InitSlot();
                break;
            case GameState.GameOver:
                ScoreManager.Instance.Loose();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}

public enum GameState
{
    GenerateGrid,
    PlayerTurn,
    GameOver,
}
