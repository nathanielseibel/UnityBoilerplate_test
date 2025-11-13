using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StorySlideManager : MonoBehaviour
{
    public Image slideImage;
    public Sprite[] slideSprites;
    public Button nextButton;
    public Button skipButton;

    private int currentSlideIndex = 0;

    void Start()
    {
        DisplayCurrentSlide();
        if (nextButton != null)
        {
            nextButton.onClick.AddListener(NextSlide);
        }
        if (skipButton != null)
        {
            skipButton.onClick.AddListener(SkipIntro);
        }
    }

    void DisplayCurrentSlide()
    {
        if (currentSlideIndex < slideSprites.Length)
        {
            slideImage.sprite = slideSprites[currentSlideIndex];
            
        }
        else
        {
            // All slides shown, transition to main game
            SceneManager.LoadScene("Game"); 
        }
    }

    public void NextSlide()
    {
        currentSlideIndex++;
        DisplayCurrentSlide();
    }

    public void SkipIntro()
    {
        SceneManager.LoadScene("Game");
    }
}
