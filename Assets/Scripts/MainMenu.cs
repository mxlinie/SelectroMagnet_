﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static MainMenu Instance;

    public GameObject pausePanel;
    public GameObject PlayerRR;


    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LevelSelect(string levelName)
    {
        SceneManager.LoadScene(levelName);
        Time.timeScale = 1.0f;
    }

    public void Resume()
    {
        Time.timeScale = 1.0f;
        pausePanel.gameObject.SetActive(false);

    }

    public void QuitGame()
    {
        Debug.Log("QUIT");  // Hitting quit button in unity wont close application
        Application.Quit();
    }
}
