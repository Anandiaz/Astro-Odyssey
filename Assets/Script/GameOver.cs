using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject gameOver;
    public void mati()
    {
        gameOver.SetActive(true);
        Time.timeScale = 0;
    }
    public void Quit()
    {
        SceneManager.LoadScene("Main_Menu");
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }
}
