using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject dam;
    [SerializeField] private TextMeshProUGUI punText;

    public int currentIndex = 0;

    //list of puns put into an array
    private string[] puns = new string[] { 
        "The dam has fallen. So has your defense.", 
        "You couldn't hold it together...dam.",
        "The cracks in your strategy really showed.",
        "When it rains, it pours... especially through broken dams.",
        "You put up a good fight, but the dam's run dry.",
        "The beavers are filing a complaint.",
        "That dam was your responsibility. WAS.",
        "Looks like you're all washed up as a dam keeper!",
        "Your defense was full of holes. Now, so is the dam!",
        "The only thing that broke faster than the dam? Your spirit.",
        "Error 404: Dam not found.",
        "The dam's gone. Water we gonna do now?",
        "The only thing flowing faster than that water? Your tears.",
        "The dam has left the chat.",
        "Instructions unclear: Dam now in multiple pieces."
    };

    public void SetRandomText() // Make this public if you want to call it from a button
    {
        if (puns != null && puns.Length > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, puns.Length); // Get a random index
            punText.text = puns[randomIndex]; // Assign the text
        }
        else
        {
            Debug.Log("Text options array is empty or null!");
        }
    }

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

        //Set a new random game over text upon starting the game
        SetRandomText();   
    }

    public void ShowGameOverUI()
    {
        //freeze the game with coroutine
        StartCoroutine(DelayFreeze());
        gameOverUI.SetActive(true);
        Debug.Log("Game is over!");

        //change the pun text to one of the random indexes in the puns array

        

        
    }

    //coroutine that delays the game for 2 seconds freezes it
    IEnumerator DelayFreeze()
    {
        Debug.Log("Starting delayed action...");
        // Wait for 3 seconds
        yield return new WaitForSeconds(2f);
        Time.timeScale = 0f;
        Debug.Log("Delayed action complete after 2 seconds!");
    }
}
