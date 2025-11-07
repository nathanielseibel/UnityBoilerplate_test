
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverUI;
    public void RestartGame()
    {
        SceneManager.LoadScene("Game");
    }
    public void QuitGame()
    {
        SceneManager.LoadScene("Scenes/Main Menu");
        
    }
    void Start()
    {
        if (gameOverUI != null)
        {
           gameOverUI.SetActive(false);
        }
    }
    public void ShowGameOverUI()
    {
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }

        Time.timeScale = 0f;
    }
    
}
