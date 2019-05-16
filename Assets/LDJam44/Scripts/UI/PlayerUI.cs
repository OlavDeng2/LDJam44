using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerUI : MonoBehaviour
{
    [Header("HUD")]
    public Text healthText;
    public Text ammoText;

    [Header("Canvases")]
    public GameObject gameMenuCanvas;
    public GameObject gameOverCanvas;

    [Header("Scenes")]
    public string mainMenu = "MainMenu";

    [Header("Data")]
    public bool gameOver = false;
    public bool isPaused = false;


    public void UpdateAmmoText(int currentAmmoInMag, int totalAmmo)
    {
        ammoText.text = currentAmmoInMag + "/" + totalAmmo;
    }

    public void UpdateHealthText(float health)
    {
        healthText.text = "Health: " + health;

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
