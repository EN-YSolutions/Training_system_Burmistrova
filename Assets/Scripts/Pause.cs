using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public bool PauseGame;
    public GameObject theoryWin;
    public GameObject pauseGameMenu;

    void Start()
    {
        Resume();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(PauseGame)
            {
                Resume();
            }
            else
            {
                PauseG();
            }
        }
    }

    public void Resume()
    {
        pauseGameMenu.SetActive(false);
        theoryWin.SetActive(false);
        Time.timeScale = 1f;
        PauseGame = false;
    }

    public void Theory()
    {
        theoryWin.SetActive(true);
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            theoryWin.SetActive(false);
            PauseG();
        }
    }

    public void PauseG()
    {
        pauseGameMenu.SetActive(true);
        Time.timeScale = 0f;
        PauseGame = true;
    }
}
