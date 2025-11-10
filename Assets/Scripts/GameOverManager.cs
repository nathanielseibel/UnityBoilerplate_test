using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject dam;
    [SerializeField] private TextMeshProUGUI punText;
    public int currentIndex = 0;

    public string[] puns = new string[] { 
        "The dam has fallen. So has your defense.", 
        "You couldn't hold it together...dam.",
        "The cracks in your strategy really showed.",
        "When it rains, it pours... especially through broken dams.",
        "You put up a good fight, but the dam's run dry.",
        "The beavers are filing a complaint.",
        "That dam was your responsibility. WAS.",
        "Looks like you're all washed up as a dam keeper!",
        "Your defense was full of holes. Now so is the dam.",
        "The only thing that broke faster than the dam? Your spirit.",
        "Error 404: Dam not found.",
        "The dam's gone. Water we gonna do now?",
        "The only thing flowing faster than that water? Your tears.",
        "The dam has left the chat.",
        "Instructions unclear: Dam now in multiple pieces."
    };

    public void RestartGame()
    {
        SceneManager.LoadScene("Game");
        Time.timeScale = 1.0f;
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
        //freeze the game
        //waitforseconds then freeze


        StartCoroutine(DelayFreeze());
        gameOverUI.SetActive(true);
        Debug.Log("Game is over!");
        

        
    }

    IEnumerator DelayFreeze()
    {
        Debug.Log("Starting delayed action...");
        // Wait for 3 seconds
        yield return new WaitForSeconds(3f);
        Time.timeScale = 0f;
        Debug.Log("Delayed action complete after 3 seconds!");
    }
}
