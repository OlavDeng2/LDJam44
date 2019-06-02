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
    public Text ammoText;

    [Header("Inventory")]
    public Inventory inventory;
    public GameObject HotBar;
    public GameObject inventoryPanel;


    [Header("playerTalking")]
    public Text talkText;
    public GameObject talkTextCanvas;
    public bool isTalking = false;

    [Header("Canvases")]
    public GameObject gameMenuCanvas;
    public GameObject gameOverCanvas;
    public GameObject gameWinCanvas;
    public GameObject loadingScreen;


    [Header("Scenes")]
    public string mainMenu = "MainMenu";

    [Header("Data")]
    public bool gameOver = false;
    public bool isPaused = false;
    public InventorySlot[] inventorySlots;

    private void Start()
    {
        gameMenuCanvas.SetActive(false);
        gameOverCanvas.SetActive(false);
        loadingScreen.SetActive(false);

        inventorySlots = GetComponentsInChildren<InventorySlot>(true);

        inventory.inventoryUpdated += Inventory_inventoryUpdated;
    }

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
    public void GameWin()
    {
        if (gameWinCanvas)
        {
            gameWinCanvas.SetActive(true);
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
        loadingScreen.SetActive(true);
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
            this.GetComponent<CanvasGroup>().blocksRaycasts = true;
        }

        else if(inventoryPanel.activeSelf)
        {
            inventoryPanel.SetActive(false);
            this.GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
    }

    public void CloseInventory()
    {
        
    }

    private void Inventory_inventoryUpdated(object sender, InventoryEventsArgs e)
    {
        for(int x = 0; x < inventory.inventoryItems.GetLength(0); x++)
        {
            for(int y = 0; y < inventory.inventoryItems.GetLength(1); y++)
            {
                if (inventory.inventoryItems[x, y] != null)
                {
                    InventorySlot slot = inventorySlots[x + y];
                    slot.AddItem(inventory.inventoryItems[x, y]);                    
                }
            }
        }
    }
}

