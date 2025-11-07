
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene("Scenes/Game");
    }
    public void QuitGame()
    {
        SceneManager.LoadScene("Scenes/Main Menu");
        
    }
}
