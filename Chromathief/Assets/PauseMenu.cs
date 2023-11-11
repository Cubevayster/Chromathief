using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;

    public GameObject pauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // normal rate
        gameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true); // display pause menu
        Time.timeScale = 0f; // freeze game
        gameIsPaused = true;

    }

    public void RestartGame()
    {
        Debug.Log("restarting game...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f; // normal speed
    }

    public void QuitGame()
    {
        Resume();
        SceneManager.LoadScene("MainMenu");
    } 
}
