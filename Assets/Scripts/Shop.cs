using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{

    [Header("UI")]
    public GameObject ui;

    [Header("Shop Prices")]
    public int bulletsPerHealth = 1;
    public int healthPerBuy = 30;

    [Header("Data")]
    public Player player;
    public bool canOpenStore = false;
    public bool storeIsOpen = false;

    private void Start()
    {
        ui.SetActive(false);
    }

    private void Update()
    {

        if(canOpenStore)
        {
            //when a certain button is pressed, open the store
            if (Input.GetButtonDown("Interact"))
            {
                if (!storeIsOpen)
                {
                    OpenStore();
                    
                }

                else if(storeIsOpen)
                {
                    CloseStore();
                }
            }
        }
    }

    public void BuyBullets()
    {
        if(player)
        {
            if (player.health > healthPerBuy)
            {
                player.totalAmmo += bulletsPerHealth * healthPerBuy;
                player.health -= healthPerBuy;
            }
        }    
    }

    public void OpenStore()
    {
        //pause the game
        Time.timeScale = 0;
        storeIsOpen = true;
        ui.SetActive(true);
    }

    public void CloseStore()
    {
        //unpause game
        Time.timeScale = 1;
        storeIsOpen = false;
        ui.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.GetComponent<Player>();

        if(player)
        {
            canOpenStore = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        player = collision.GetComponent<Player>();

        if (player)
        {
            canOpenStore = false;
        }
    }
}
