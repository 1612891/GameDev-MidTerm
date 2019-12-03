using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void Endless()
    {
        PlayerPrefs.SetInt("mode", 0);
        SceneManager.LoadScene(1);
    }

    public void Survival()
    {
        PlayerPrefs.SetInt("mode", 1);
        SceneManager.LoadScene(1);
    }

    public void Help()
    {
        SceneManager.LoadScene(3);
    }

    public void BackMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
