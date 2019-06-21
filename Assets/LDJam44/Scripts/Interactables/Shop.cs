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
    public int maxItems = 5;
    public int minItems = 1;

    [Header("Data")]
    public bool canOpenStore = false;
    public bool storeIsOpen = false;



    private void Start()
    {

        shopInventory.itemAdded += Inventory_ItemAdded;
        shopInventory.itemRemoved += Inventory_ItemRemoved;

        shopUI.SetActive(false);
        gameManager = FindObjectOfType<GameManager>();

        //Get the amount of items
        int itemCount = UnityEngine.Random.Range(minItems, maxItems);

        if (itemCount >= shopInventory.inventorySlots.Length)
        {
            //Set to max just incase there are not enough inventory slots
            itemCount = shopInventory.inventorySlots.Length;


        }

        // loop as many times as there are items to add
        for (int i = 0; i < itemCount; i++)
        {
            GameObject itemToAddToInv = Instantiate(shopItems[UnityEngine.Random.Range(0, shopItems.Length)], this.transform);
            itemToAddToInv.transform.position = this.transform.position;

            if (shopInventory.inventorySlots[i].item == null)
            {
                //Get random amount to add to the inventory slot between 1 and max stack amount (so should always be 1 if max stack amount is 1)
                shopInventory.inventorySlots[i].AddItem(itemToAddToInv, UnityEngine.Random.Range(1, itemToAddToInv.GetComponent<Item>().maxStackCount)) ;
                itemToAddToInv.SetActive(false);
            }

            else if (shopInventory.inventorySlots[i].item != null)
            {
                break;
            }
        }


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
                playerInventory.inventorySlots[i].priceText.enabled = true;
                playerInventory.inventorySlots[i].priceText.text = (itemToAdd.GetComponent<Item>().amount * itemToAdd.GetComponent<Item>().sellPrice).ToString();
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

    private void Inventory_ItemAdded(object sender, InventoryEventsArgs e)
    {
        if(storeIsOpen)
        {
            player.health += e.Item.GetComponent<Item>().amount * e.Item.GetComponent<Item>().sellPrice;
            e.InvSlot.priceText.enabled = true;
            e.InvSlot.priceText.text = (e.InvSlot.item.GetComponent<Item>().amount * e.InvSlot.item.GetComponent<Item>().buyPrice).ToString();
        }
    }

    private void Inventory_ItemRemoved(object sender, InventoryEventsArgs e)
    {
        if(storeIsOpen)
        {
            player.health -= e.Item.GetComponent<Item>().amount * e.Item.GetComponent<Item>().buyPrice;
            e.InvSlot.priceText.enabled = false;
        }
    }
}
