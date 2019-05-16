using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//This script is for in game UI
public class UIManager : MonoBehaviour
{
    [Header("Canvases")]
    public GameObject gameMenuCanvas;
    public GameObject gameOverCanvas;

    [Header("Scenes")]
    public string mainMenu = "MainMenu";

    [Header("Data")]
    public bool gameOver = false;
    public bool isPaused = false;

    private void Update()
    {
        //if escape is pressed, pause the game
        if(Input.GetKeyDown(KeyCode.Escape) && !gameOver)
        {
           
            if(!isPaused)
            {
                PauseGame();
            }

            else if(isPaused)
            {
                UnPauseGame();
            }
        }
    }

    public void GameOver()
    {
        if (gameOverCanvas)
        {
            gameOverCanvas.SetActive(true);
            Time.timeScale = 0;
            gameOver = true;
        }
    }

    public void RestartGame()
    {
        //Reload this scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(mainMenu);
    }

    public void PauseGame()
    {
        if (gameMenuCanvas)
        {
            gameMenuCanvas.SetActive(true);
        }
        Time.timeScale = 0;
        isPaused = true;

    }

    public void UnPauseGame()
    {
        if (gameMenuCanvas)
        {
            gameMenuCanvas.SetActive(false);
        }
        Time.timeScale = 1;
        isPaused = false;
    }
}
