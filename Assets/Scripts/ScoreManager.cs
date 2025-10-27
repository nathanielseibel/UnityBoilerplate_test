using UnityEngine;
using TMPro; // Import TextMeshPro namespace

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance; // Singleton for easy access

    [Header("UI References")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI hiScoreText;

    private int currentScore = 0;
    private int hiScore = 0;

    private void Awake()
    {
        // Singleton pattern - only one ScoreManager exists
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Load the high score from PlayerPrefs (saved data)
        hiScore = PlayerPrefs.GetInt("HiScore", 0);

        // Update UI
        UpdateScoreUI();
        UpdateHiScoreUI();
    }

    // Call this method to add points
    public void AddScore(int points)
    {
        currentScore += points;
        UpdateScoreUI();

        // Check if we beat the high score
        if (currentScore > hiScore)
        {
            hiScore = currentScore;
            PlayerPrefs.SetInt("HiScore", hiScore);
            PlayerPrefs.Save();
            UpdateHiScoreUI();

            Debug.Log("New High Score!");
        }
    }

    // Call this to reset the current score (e.g., when restarting the game)
    public void ResetScore()
    {
        currentScore = 0;
        UpdateScoreUI();
    }

    // Get current score
    public int GetScore()
    {
        return currentScore;
    }

    // Get high score
    public int GetHiScore()
    {
        return hiScore;
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + currentScore;
        }
    }

    private void UpdateHiScoreUI()
    {
        if (hiScoreText != null)
        {
            hiScoreText.text = "Hi Score: " + hiScore;
        }
    }
}