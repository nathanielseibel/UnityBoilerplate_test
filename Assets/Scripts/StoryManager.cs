using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StoryManager : MonoBehaviour
{

    [SerializeField] private GameObject storyUI;
    [SerializeField] private GameObject storyImage1;
    [SerializeField] private GameObject storyImage2;
    [SerializeField] private GameObject storyImage3;
    [SerializeField] private GameObject storyImage4;
    [SerializeField] private GameObject genreText;

    [SerializeField] private GameObject next;
    [SerializeField] private GameObject next2;
    [SerializeField] private GameObject next3;
    [SerializeField] private GameObject start;

    [SerializeField] private GameObject skip;


    // Start is called before the first frame update

    //Set UI avtive and pop up the Title.
    void Start()
    {
        //Set the attached Object to active
        storyUI.SetActive(true);
        storyImage1.SetActive(true);
        storyImage2.SetActive(false);
        storyImage3.SetActive(false);
        storyImage4.SetActive(false);
        genreText.SetActive(true);
        next.SetActive(true);
        next2.SetActive(false);
        next3.SetActive(false);
        skip.SetActive(true);
        start.SetActive(false);

    }

    //First Story - attach to Next onclick!
    public void StoryPart1()
    {
        storyImage1.SetActive(false);
        storyImage2.SetActive(true);
        genreText.SetActive(false);
        next.SetActive(false);
        next2.SetActive(true);
    }
    //Second Image - attach to NextImage1 onclick!
    public void StoryPart2()
    {
        storyImage2.SetActive(false);
        storyImage3.SetActive(true);
        next2.SetActive(false);
        next3.SetActive(true);
    }

    //Third Image - attach to NextImage2 onclick!
    public void StoryPart3()
    {
        storyImage3.SetActive(false);
        storyImage4.SetActive(true);
        next3.SetActive(false);
        start.SetActive(true);
        
    }

    //Load game - attach to Start and Skip!
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
