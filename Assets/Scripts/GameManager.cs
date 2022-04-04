using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
    }

    public bool IsGamePaused()
    {
        if (Time.timeScale >= 0.5f) 
            return false;
        else
            return true;
    }
}
