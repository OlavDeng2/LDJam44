using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : Character
{
    [Header("UI")]
    public PlayerUI playerUI;
    public Inventory inventory;

    [Header("PlayerTalk")]
    string nothingToInteractWithText = "nothing";

    [Header("Gun")]
    public ObjectPool bulletPool;

    [Header("Canvases")]
    public GameObject gameMenuCanvas;
    public GameObject gameOverCanvas;

    [Header("Scenes")]
    public string mainMenu = "MainMenu";

    [Header("Data")]
    public Interactable interactable = null;
    public TutorialPoint tutorial = null;
    public Item currentItem = null;

    private void Start()
    {
        inventory.itemSelected += Inventory_itemSelected;
    }



    // Update is called once per frame
    void Update()
    {

        //update UI
        playerUI.UpdateHealthText(health);

        //keep counter going to keep track of when shot was last fired

        //Handle input
        MoveCharacter(Vector3.Normalize(new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0)));
        Vector3 lookDirection = Vector3.Normalize((Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10)) - this.transform.position);

        //Interact with something
        if (Input.GetButtonDown("Interact"))
        {
            if(interactable == null)
            {
                if(!playerUI.isTalking)
                {
                    playerUI.InteractWithObjectTalk(nothingToInteractWithText);
                }
                else if(playerUI.isTalking)
                {
                    playerUI.CloseTalk();
                }
            }
            else if (interactable != null)
            {
                interactable.tryToInteract();
            }

            
        }

        if(Input.GetButtonDown("Inventory"))
        {

            playerUI.ToggleInventory();
        }

        //if escape is pressed, pause the game
        if (Input.GetKeyDown(KeyCode.Escape) && !playerUI.gameOver)
        {

            if (!playerUI.isPaused)
            {
                playerUI.PauseGame();
                
            }

            else if (playerUI.isPaused)
            {
                playerUI.UnPauseGame();
            }
        }

        //use item
        if (Input.GetButton("Fire1"))
        {
            if (currentItem != null)
            {
                currentItem.UseItem();


                //Cycle gun if it is a gun in semi auto
                if (currentItem != null && currentItem is Weapon)
                {
                    Weapon weapon = currentItem as Weapon;
                    if (weapon.availableFireModes[weapon.currentFireMode] == Weapon.FireMode.SemiAuto)
                    {
                        weapon.isCyclingGun = true;
                    }
                }

            }
        }

        
        if(Input.GetButtonUp("Fire1"))
        {
            //Cycle gun if it is a gun in manual or semi auto
            if(currentItem != null && currentItem is Weapon)
            {
                Weapon weapon = currentItem as Weapon;
                if(weapon.availableFireModes[weapon.currentFireMode] == Weapon.FireMode.Manual)
                {
                    weapon.isCyclingGun = true;
                }
            }
        }


        if (Input.GetButtonDown("Reload"))
        {

            if (currentItem is Weapon)
            {
                Weapon weapon = currentItem as Weapon;
                weapon.isReloading = true;
            }
        }

        if (Input.GetButtonDown("Switch Fire Mode"))
        {
            if (currentItem is Weapon)
            {
                Weapon weapon = currentItem as Weapon;
                {
                    weapon.ChangeFireMode();
                }
            }
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Item item = collision.gameObject.GetComponent<Item>();
        
        if (item != null)
        {
            inventory.PickupItem(item.inventoryItem,collision.gameObject, item.amount);
        }
    }

    //Get the item from inventory if item selected and spawn the prefab in the right spot
    private void Inventory_itemSelected(object sender, InventoryEventsArgs e)
    {
        //Remove old item
        if(currentItem)
        {
            currentItem.gameObject.SetActive(false);
            currentItem = null;
        }

        //Spawn item based on inventory item prefab
        InventoryItem invItem = e.Item;
        if(invItem!= null)
        {
            e.InvSlot.itemGameobject.SetActive(true);
            currentItem = e.InvSlot.itemGameobject.GetComponent<Item>();
            currentItem.GetComponent<Item>().player = this;
            currentItem.GetComponent<Item>().invSlot = e.InvSlot;

            currentItem.GetComponent<Collider2D>().enabled = false;
        }
    }
}
