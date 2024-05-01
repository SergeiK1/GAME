using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    // Start is called before the first frame update

        public GameObject pauseMenu;
        public static bool isActive = true;
        

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (isActive) 
            {
                PauseGame();
                Debug.Log("Paused");
            } 

            else 
            {
                StartGame();
                Debug.Log("Paused");
            }
            
        }
        
    }

    

    private void PauseGame() 
    {

        // LOAD PAUSE SCREEN
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isActive = false;
    }
    public void StartGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f; 
        isActive = true;
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
 