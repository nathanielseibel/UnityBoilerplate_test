using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Story and Tut");
        //Time starts
        Time.timeScale = 1.0f;
    }
    public void QuitGame()
    {
        Application.Quit();
        
    }
    //SceneManager is an API from Unity that allows us to manage scenes
    //loadScene is a function that loads a scene by its name or index
    //"Game" is the name of the scene we want to load
}
