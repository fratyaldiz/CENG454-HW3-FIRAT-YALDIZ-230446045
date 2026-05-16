using UnityEngine;
using UnityEngine.UI;

// listen score events and show score on screen
public class ScoreUI : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    private int totalScore =0;

    private void OnEnable()
    {
        GameEvents.OnScoreChanged+= AddScore;
    }

    private void OnDisable()
    {
        GameEvents.OnScoreChanged -=AddScore;
    }

    private void Start()
    {
        UpdateText();
    }

    private void AddScore(int value)
    {
        totalScore +=value;
        UpdateText();
    }

    private void UpdateText()
    {
        if (scoreText != null)
        {
            scoreText.text ="Score: "+totalScore;
        }
    }
}