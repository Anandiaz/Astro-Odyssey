using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button playButton;
    public Button quitButton;
    public int currentSelection = 0; // Initialize the current selection to 0 (play button)
    public Animator playAnimator;
    public Animator quitAnimator;
    public AudioManager audioManager;

    void Start()
    {
        // Initialize the buttons
        playButton.Select();
        quitButton.Select();
    }

    void Update()
    {
        // Move the selection up
        if (Input.GetKeyDown(KeyCode.UpArrow) && currentSelection > 0)
        {
            currentSelection--;
        }

        // Move the selection down
        if (Input.GetKeyDown(KeyCode.DownArrow) && currentSelection < 1)
        {
            currentSelection++;
        }

        // Select the button based on the current selection
        if (currentSelection == 0)
        {
            playButton.Select();
            playAnimator.SetTrigger("Highlighted");
        }
        else
        {
            quitButton.Select();
            quitAnimator.SetTrigger("Highlighted");
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Stage 1");
        Destroy(audioManager.gameObject);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}