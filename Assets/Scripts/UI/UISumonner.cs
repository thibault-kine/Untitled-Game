using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISumonner : MonoBehaviour
{
    private bool summonMenu;
    
    private GameObject menu;
    
    // Start is called before the first frame update
    void Start()
    {
        // cherche le canvas nommé "Menu"
        menu = GameObject.Find("Menu");

        summonMenu = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            summonMenu = !summonMenu;
        }

        Cursor.lockState = summonMenu ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = summonMenu;
        
        // active ou désactive le canvas
        menu.SetActive(summonMenu);
        
        // Pause
        if(summonMenu)
            PauseGame();
        else
            ResumeGame();
    }

    void PauseGame()
    {
        Time.timeScale = 0.01f;
    }
    
    void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
