using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TheEnd : MonoBehaviour
{
    public GameObject theEnd;
    public void tamat()
    {
        theEnd.SetActive(true);
        Time.timeScale = 0;
    }
    public void Quit()
    {
        SceneManager.LoadScene("Main_Menu");
    }
}
