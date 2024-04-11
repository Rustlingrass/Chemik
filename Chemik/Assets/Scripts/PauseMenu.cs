using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject interactiveInterface;
    public static bool isPaused = false;

    void Start()
    {
        ResumeGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            if (isPaused) 
            {
                ResumeGame();
                isPaused = false;
            } else 
            {
                PauseGame();
                isPaused = true;
            }
        }
    }
    public void PauseGame() 
    {
        interactiveInterface.SetActive(false);
        pauseMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
        isPaused = true;
    }
    public void ResumeGame() 
    {
        interactiveInterface.SetActive(true);
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        isPaused = false;
    }
    public void BackToMainMenu() 
    {
        SceneManager.LoadScene("MainMenu");
        isPaused = false;
    }
    public void QuitApp()
    {
        Application.Quit();
    }
}
