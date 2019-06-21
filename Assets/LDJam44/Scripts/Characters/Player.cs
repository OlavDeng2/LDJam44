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

    [Header("PlayerTalk")]
    string nothingToInteractWithText = "nothing";

    [Header("Canvases")]
    public GameObject gameMenuCanvas;
    public GameObject gameOverCanvas;

    [Header("Scenes")]
    public string mainMenu = "MainMenu";

    [Header("Data")]
    public Interactable interactable = null;
    public TutorialPoint tutorial = null;
    public Item currentItem = null;
    public bool inventoryOpen = false;

    private void Start()
    {
        inventory.itemSelected += Inventory_itemSelected;
        
        
    }



    // Update is called once per frame
    void Update()
    {

        //update UI
        playerUI.UpdateHealthText(health);

        if(health <= 0)
        {
            KillCharacter();
        }


        //can only do if the inventory is not open and game is not paused
        if (!inventoryOpen && !gameManager.isPaused)
        {
            //Handle input
            MoveCharacter(Vector3.Normalize(new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0)));
            Vector3 lookDirection = Vector3.Normalize((Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10)) - this.transform.position);

            //Interact with something
            if (Input.GetButtonDown("Interact"))
            {
                if (interactable == null)
                {
                    if (!playerUI.isTalking)
                    {
                        playerUI.InteractWithObjectTalk(nothingToInteractWithText);
                    }
                    else if (playerUI.isTalking)
                    {
                        playerUI.CloseTalk();
                    }
                }
                else if (interactable != null)
                {
                    interactable.tryToInteract();
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


            if (Input.GetButtonUp("Fire1"))
            {
                //Cycle gun if it is a gun in manual or semi auto
                if (currentItem != null && currentItem is Weapon)
                {
                    Weapon weapon = currentItem as Weapon;
                    if (weapon.availableFireModes[weapon.currentFireMode] == Weapon.FireMode.Manual)
                    {
                        weapon.isCyclingGun = true;
                    }

                    else if (weapon.availableFireModes[weapon.currentFireMode] == Weapon.FireMode.SemiAuto)
                    {
                        weapon.canUseItem = true;
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

        //set move to 0 if the game is paused or inventory is open
        else if (inventoryOpen || gameManager.isPaused)
        {
            MoveCharacter(Vector3.Normalize(new Vector3(0, 0, 0)));
        }

        //Only do if game is not paused and not over
        if (!gameManager.isPaused && !gameManager.gameOver)
        {
            if (Input.GetButtonDown("Inventory"))
            {
                playerUI.ToggleInventory();
                if(playerUI.inventoryPanel.activeSelf)
                {
                    inventoryOpen = true;
                }

                else if(!playerUI.inventoryPanel.activeSelf)
                {
                    inventoryOpen = false;
                }
            }
        }
        
        //only do if game is not over
        if(!gameManager.gameOver)
        {
            //if escape is pressed, pause the game
            if (Input.GetKeyDown(KeyCode.Escape) && !playerUI.gameOver)
            {
                if (!gameManager.isPaused)
                {
                    playerUI.PauseGame();
                    gameManager.PauseGame();
                }

                else if (gameManager.isPaused)
                {
                    gameManager.UnPauseGame();
                    playerUI.UnPauseGame();
                }
            }
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Item item = collision.gameObject.GetComponent<Item>();
        
        if (item != null)
        {
            collision.gameObject.transform.SetParent(this.gameObject.transform);
            collision.gameObject.transform.localPosition = new Vector3(0, 0, 0);
            inventory.PickupItem(collision.gameObject, item.amount);
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
        GameObject invItem = e.Item;
        if(invItem!= null)
        {


            invItem.SetActive(true);
            currentItem = invItem.GetComponent<Item>();
            currentItem.character = this;
            currentItem.invSlot = e.InvSlot;

            currentItem.GetComponent<Collider2D>().enabled = false;
        }
    }

    public override void KillCharacter()
    {
        alive = false;
        playerUI.GameOver();
        gameManager.PauseGame();
    }
}
