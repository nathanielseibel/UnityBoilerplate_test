using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // syntax of functions
    //ReturnType FunctionName(ParameterType parameterName,...) {...function body...}
    //void means the function doesn't return any value
    //StartGame is function name () means the function doesn't take any parameters and {}is function body
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
    public void QuitGame()
    {
        Application.Quit();
        
    }
    //SceneManager is an API from Unity that allows us to manage scenes
    //loadScene is a function that loads a scene by its name or index
    //"Game" is the name of the scene we want to load
}
