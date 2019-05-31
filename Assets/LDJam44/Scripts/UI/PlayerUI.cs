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
    public Transform inventoryPanelRegular;
    public Transform inventoryPanelOpen;


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

    private void Start()
    {
        gameMenuCanvas.SetActive(false);
        gameOverCanvas.SetActive(false);
        loadingScreen.SetActive(false);

        inventory.itemAdded += InventoryScript_ItemAdded;
        inventory.itemRemoved += InventoryScript_ItemRemoved;
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
    private void InventoryScript_ItemAdded(object sender, InventoryEventsArgs e)
    {

        foreach (Transform slot in inventoryPanelRegular)
        {
            Transform imageTransform = slot.GetChild(0).GetChild(0);
            Image image = imageTransform.GetComponent<Image>();
            ItemDragHandler itemDragHandler = imageTransform.GetComponent<ItemDragHandler>();

            Debug.Log("Test");

            if (!image.enabled)
            {
                image.enabled = true;
                image.sprite = e.Item.Image;
                itemDragHandler.item = e.Item;

                break;
            }
        }
    }


    private void InventoryScript_ItemRemoved(object sender, InventoryEventsArgs e)
    {
        foreach (Transform slot in inventoryPanelRegular)
        {
            Transform imageTransform = slot.GetChild(0).GetChild(0);
            Image image = imageTransform.GetComponent<Image>();
            ItemDragHandler itemDragHandler = imageTransform.GetComponent<ItemDragHandler>();

            if (itemDragHandler.item.Equals(e.Item))
            {
                image.enabled = false;
                image.sprite = null;
                itemDragHandler.item = null;

                break;
            }
        }
    }

}
