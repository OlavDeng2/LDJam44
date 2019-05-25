using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : Interactable
{

    [Header("UI")]
    public GameObject ui;

    [Header("Shop Prices")]
    public int bulletsPerHealth = 1;
    public int healthPerBuy = 30;


    public bool canOpenStore = false;
    public bool storeIsOpen = false;

    private void Start()
    {
        ui.SetActive(false);
    }

    public void BuyBullets()
    {
        if (player)
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

    public override void Interact()
    {
        base.Interact();
        
        if (!storeIsOpen)
        {
            OpenStore();

        }

        else if (storeIsOpen)
        {
            CloseStore();
        }
    }
}
