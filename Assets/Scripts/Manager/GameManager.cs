using UnityEngine;

public enum GameState
{
    Win,
    Lose,
    Playing,
    Pause
}

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }
    
    public GameState currentState = GameState.Playing;

    public void ChangeState(GameState newState)
    {
        currentState = newState;
    }
}