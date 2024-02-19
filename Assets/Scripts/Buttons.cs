using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public void BacktoMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Play()
    {
        SceneManager.LoadScene("CourseChoice");
    }

    public void Continue()
    {
        SceneManager.LoadScene("CourseChoice");
    }

    public void Settings()
    {
        SceneManager.LoadScene("CourseChoice");
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void ToLevelOne()
    {
        SceneManager.LoadScene("LevelOne");
    }

    public void ToLevelTwo()
    {
        SceneManager.LoadScene("LevelOne");
    }
}
