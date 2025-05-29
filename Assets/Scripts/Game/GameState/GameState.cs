using System;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public GameStatus CurrentState; // TODO: make get set

    public event Action OnGameStarted;
    public event Action OnGameFinished;

    private void Start()
    {
        CurrentState = GameStatus.TapToPlay;
    }

    public void StartGame()
    {
        CurrentState = GameStatus.Playing;
        OnGameStarted?.Invoke();
        
        Debug.Log("Game Started!");
    }
    
    public void FinishGame()
    {
        CurrentState = GameStatus.Playing;
        OnGameFinished?.Invoke();
        
        Debug.Log("Game Finished!");
    }
}
