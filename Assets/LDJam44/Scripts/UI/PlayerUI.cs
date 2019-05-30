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

    [Header("Inventory")]
    public Inventory inventory;

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
        Transform inventoryPanel = transform.Find("Regular");
        foreach(Transform slot in inventoryPanel)
        {
            Image image = slot.GetChild(0).GetChild(0).GetComponent<Image>();

            if(!image.enabled)
            {
                image.enabled = true;
                image.sprite = e.Item.Image;

                //TODO: Store a reference to the item

                break;
            }
        }
    }

}
