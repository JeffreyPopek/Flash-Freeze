using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        //sound
        FindObjectOfType<AudioManager>().Play("ButtonPress");

        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");

        //sound
        FindObjectOfType<AudioManager>().Play("ButtonPress");

        Application.Quit();
    }

    public void Menu()
    {
        //sound
        FindObjectOfType<AudioManager>().Play("ButtonPress");

        SceneManager.LoadScene(0);
    }
}
