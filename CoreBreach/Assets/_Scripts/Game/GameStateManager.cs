using System;
using UnityEngine;

// keeps the game state in one place.
// when win or lose, it tell everyone via events and stop the game.
public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance { get; private set; }

    public enum GameState
    {
        Playing,
        Won,
        Lost
    }

    public GameState CurrentState { get; private set; } =GameState.Playing;

    public event Action OnGameWon;
    public event Action OnGameLost;

    private void Awake()
    {
        if (Instance !=null && Instance !=this)
        {
            Destroy(gameObject);
            return;
        }
        Instance =this;
    }

    private void OnEnable()
    {
        if (Core.Instance!= null)
        {
            Core.Instance.OnCoreDestroyed +=HandleCoreDestroyed;
        }
        GameEvents.OnWaveCompleted+= HandleWaveCompleted;
    }

    private void Start()
    {
        if (Core.Instance !=null)
        {
            Core.Instance.OnCoreDestroyed -= HandleCoreDestroyed;
            Core.Instance.OnCoreDestroyed +=HandleCoreDestroyed;
        }
    }

    private void OnDisable()
    {
        if (Core.Instance!= null)
        {
            Core.Instance.OnCoreDestroyed -=HandleCoreDestroyed;
        }
        GameEvents.OnWaveCompleted-= HandleWaveCompleted;
    }

    private void HandleCoreDestroyed()
    {
        if (CurrentState !=GameState.Playing) return;

        CurrentState =GameState.Lost;
        OnGameLost?.Invoke();
        StopGame();
        Debug.Log("GAME OVER - Player Lost");
    }

    private int waveCount= 0;

    private void HandleWaveCompleted(int waveNumber)
    {
        waveCount =waveNumber;
    }

    public void DeclareVictory()
    {
        if (CurrentState!= GameState.Playing) return;

        CurrentState = GameState.Won;
        OnGameWon?.Invoke();
        StopGame();
        Debug.Log("VICTORY - Player Won");
    }

    private void StopGame()
    {
        // freeze game by setting timescale to 0
        Time.timeScale= 0f;
    }

    public void RestartGame()
    {
        Time.timeScale =1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
        );
    }
}