﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Start()
    {
        //Pauses the game in the background 
        Time.timeScale = 0;
    }

    public void PlayGame ()
    {
        SceneManager.LoadScene("Prototype 4");
        Time.timeScale = 1;
    }

    public void QuitGame ()
    {
        Debug.Log("Program has exited");
        Application.Quit();
    }
}
