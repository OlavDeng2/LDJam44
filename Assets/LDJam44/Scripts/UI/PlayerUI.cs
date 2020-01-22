using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class PlayerUI : MonoBehaviour
{
    [Header("HUD")]
    public Text healthText;
    public Slider healthBar;
    public Text ammoText;
    public GameObject ammoIndicator;

    [Header("Inventory")]
    public Inventory inventory;
    public GameObject HotBar;
    public GameObject inventoryPanel;


    [Header("playerTalking")]
    public Text talkText;
    public GameObject talkTextCanvas;
    public bool isTalking = false;

    [Header("Canvases")]
    public CanvasGroup canvasGroup;
    public GameObject gameMenuCanvas;
    public GameObject gameOverCanvas;
    public GameObject gameWinCanvas;
    public GameObject loadingScreen;


    [Header("Scenes")]
    public string mainMenu = "MainMenu";

    [Header("Data")]
    public bool gameOver = false;

    private void Start()
    {
        gameMenuCanvas.SetActive(false);
        gameOverCanvas.SetActive(false);
        loadingScreen.SetActive(false);
    }

    public void UpdateAmmoUI(int currentAmmoInMag)
    {
        ammoText.text = currentAmmoInMag.ToString();
    }

    public void HideAmmoUI()
    {
        ammoText.gameObject.SetActive(false);
        ammoIndicator.SetActive(false);
    }

    public void ShowAmmoUI()
    {
        ammoText.gameObject.SetActive(true);
        ammoIndicator.SetActive(true);
    }

    public void ReloadAmmoUI()
    {
        ammoText.text = "Reloading";
    }

    public void UpdateHealthUI(float health)
    {
        healthText.text = "Health: " + health;
        healthBar.value = health;
    }

    public void GameOver()
    {
        if (gameOverCanvas)
        {
            gameOverCanvas.SetActive(true);
            Time.timeScale = 0;
            gameOver = true;
            canvasGroup.blocksRaycasts = true;
        }
    }
    public void GameWin()
    {
        if (gameWinCanvas)
        {
            gameWinCanvas.SetActive(true);
            Time.timeScale = 0;
            gameOver = true;
            canvasGroup.blocksRaycasts = true;
        }
    }

    public void RestartGame()
    {
        //Reload this scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnToMainMenu()
    {
        loadingScreen.SetActive(true);
        SceneManager.LoadScene(mainMenu);
    }

    public void PauseGame()
    {
        if (gameMenuCanvas)
        {
            gameMenuCanvas.SetActive(true);
        }

    }

    public void UnPauseGame()
    {
        if (gameMenuCanvas)
        {
            gameMenuCanvas.SetActive(false);
        }
    }

    //Stuff for talk
    public void InteractWithObjectTalk(string text)
    {
        isTalking = true;
        talkText.text = text;
        talkTextCanvas.SetActive(true);

    }

    public void CloseTalk()
    {
        talkTextCanvas.SetActive(false);
        isTalking = false;
    }

    //Inventory stuff
    public void ToggleInventory()
    {
        if(!inventoryPanel.activeSelf)
        {
            inventoryPanel.SetActive(true);
            canvasGroup.blocksRaycasts = true;
        }

        else if(inventoryPanel.activeSelf)
        {
            inventoryPanel.SetActive(false);
            canvasGroup.blocksRaycasts = false;
        }
    }
}

