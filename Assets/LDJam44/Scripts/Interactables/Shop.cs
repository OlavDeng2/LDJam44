using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : Interactable
{

    [Header("References")]
    public GameObject shopUI;
    public Inventory shopInventory;
    public Inventory playerInventory;
    public GameManager gameManager;

    [Header("Settings")]
    public GameObject[] shopItems;

    [Header("Data")]
    public bool canOpenStore = false;
    public bool storeIsOpen = false;



    private void Start()
    {

        shopInventory.itemAdded += Inventory_ItemAdded;
        shopInventory.itemRemoved += Inventory_ItemRemoved;

        shopUI.SetActive(false);
        gameManager = FindObjectOfType<GameManager>();

        //TODO: Get a random selection of items for the store to hold based on the store loot table and place them in the inventory
    }

    


    private void Update()
    {
    }

    public void OpenStore()
    {
        storeIsOpen = true;
        shopUI.SetActive(true);
        gameManager.PauseGame();


        //Get all of the players items (this assumes player inventory length is same on the shop and the actual player)
        //Place the players items in the player inventory of the shop
        for (int i = 0; i < playerInventory.inventorySlots.Length; i++)
        {
            GameObject itemToAdd = player.inventory.inventorySlots[i].item;
            if(itemToAdd != null)
            {
                int amountToAdd = player.inventory.inventorySlots[i].amount;
                playerInventory.inventorySlots[i].AddItem(itemToAdd, amountToAdd);
                player.inventory.inventorySlots[i].RemoveItem();

            }
        }
    }

    public void CloseStore()
    {
        storeIsOpen = false;
        shopUI.SetActive(false);
        gameManager.UnPauseGame();

        //sync the new player inventory to the actual player
        //Remove all of the stuff in the player inventory of the shop
        for (int i = 0; i < playerInventory.inventorySlots.Length; i++)
        {
            GameObject itemToAdd = playerInventory.inventorySlots[i].item;
            if(itemToAdd != null)
            {
                int amountToAdd = playerInventory.inventorySlots[i].amount;
                player.inventory.inventorySlots[i].AddItem(itemToAdd, amountToAdd);
                playerInventory.inventorySlots[i].RemoveItem();

            }
        }
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


    //To do the sale/buy of items when an item was either added or removed from the shop inventory
    private void Inventory_ItemAdded(object sender, InventoryEventsArgs e)
    {
        player.health += e.Item.GetComponent<Item>().amount * e.Item.GetComponent<Item>().sellPrice;
    }

    private void Inventory_ItemRemoved(object sender, InventoryEventsArgs e)
    {
        player.health -= e.Item.GetComponent<Item>().amount * e.Item.GetComponent<Item>().buyPrice;

    }
}
