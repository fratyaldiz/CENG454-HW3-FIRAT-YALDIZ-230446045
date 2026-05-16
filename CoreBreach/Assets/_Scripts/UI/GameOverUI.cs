using UnityEngine;
using UnityEngine.UI;
using TMPro;

// shows game over or victory screen.
public class GameOverUI : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TMP_Text resultText;
    [SerializeField] private Button restartButton;

    private void Start()
    {
        if (gameOverPanel != null) gameOverPanel.SetActive(false);

        if (GameStateManager.Instance !=null)
        {
            GameStateManager.Instance.OnGameWon+= ShowVictory;
            GameStateManager.Instance.OnGameLost +=ShowGameOver;
        }

        if (restartButton !=null)
        {
            restartButton.onClick.AddListener(OnRestartClicked);
        }
    }

    private void OnDisable()
    {
        if (GameStateManager.Instance!= null)
        {
            GameStateManager.Instance.OnGameWon -= ShowVictory;
            GameStateManager.Instance.OnGameLost -= ShowGameOver;
        }
    }

    private void ShowVictory()
    {
        if (gameOverPanel != null) gameOverPanel.SetActive(true);
        if (resultText != null) resultText.text = "VICTORY!";
    }

    private void ShowGameOver()
    {
        if (gameOverPanel != null) gameOverPanel.SetActive(true);
        if (resultText !=null) resultText.text ="GAME OVER";
    }

    private void OnRestartClicked()
    {
        if (GameStateManager.Instance!= null)
        {
            GameStateManager.Instance.RestartGame();
        }
    }
}