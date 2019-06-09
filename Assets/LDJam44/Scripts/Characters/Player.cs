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
    public float bulletSpeed = 10;
    public int totalAmmo = 90;
    public int currentAmmoInMag = 30;
    public int maxAmmoInMag = 30;
    public float gunRange = 10;
    public float fireRate = 0.1f; //how many seconds between shots
    public float timeToReload = 2f;
    public LayerMask enemyLayer;

    [Header("Gun Data")]
    public float timeSinceLastShot = 0f;
    public float timeSinceReloadStart = 0f;
    public bool isReloading = false;

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
        timeSinceLastShot = fireRate;
        inventory.itemSelected += Inventory_itemSelected;
    }



    // Update is called once per frame
    void Update()
    {

        //update UI
        playerUI.UpdateAmmoText(currentAmmoInMag, totalAmmo);
        playerUI.UpdateHealthText(health);

        //keep counter going to keep track of when shot was last fired
        timeSinceLastShot += Time.deltaTime;

        //Handle input
        MoveCharacter(Vector3.Normalize(new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0)));
        Vector3 lookDirection = Vector3.Normalize((Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10)) - this.transform.position);
        //LookDirection(lookDirection );

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
            }
        }

        if(Input.GetButtonUp("Fire1"))
        {
            if(currentItem != null && currentItem is Weapon)
            {
                Weapon weapon = currentItem as Weapon;
                weapon.isCyclingGun = true;
            }
        }


        if (Input.GetButton("Reload"))
        {
            if (currentItem is Weapon)
            {
                Weapon weapon = currentItem as Weapon;
                weapon.isReloading = true;
            }
        }

        if (Input.GetButton("Switch Fire Mode"))
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
            inventory.PickupItem(item.inventoryItem, item.amount);
        }
    }

    //Get the item from inventory if item selected and spawn the prefab in the right spot
    private void Inventory_itemSelected(object sender, InventoryEventsArgs e)
    {
        //Remove old item
        if(currentItem)
        {
            Destroy(currentItem.gameObject);
            currentItem = null;
        }

        //Spawn item based on inventory item prefab
        InventoryItem invItem = e.Item;
        if(invItem!= null)
        {
            currentItem = Instantiate(invItem.itemPrefab, this.transform).GetComponent<Item>();
            currentItem.GetComponent<Item>().player = this;
            currentItem.GetComponent<Item>().player = this;


            currentItem.GetComponent<Collider2D>().enabled = false;
        }
    }
}
