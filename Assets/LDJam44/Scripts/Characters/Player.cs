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
        if(inventory.inventoryItems[0, 0] != null)
        {
            Debug.Log(inventory.inventoryItems[0, 0].amount);

        }

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
                currentItem.OnUse();
            }
        }


        if (!isReloading)
        {
            if (Input.GetButton("Fire1"))
            {
                ShootGun(lookDirection);
            }

            if (Input.GetButton("Reload"))
            {
                if (totalAmmo > 0)
                {
                    isReloading = true;
                }
            }
        }

        else if(isReloading)
        {
            timeSinceReloadStart += Time.deltaTime;
            if(timeSinceReloadStart > timeToReload)
            {
                ReloadGun();
                timeSinceReloadStart = 0f;
                isReloading = false;
            }
        }
    }

    private void ShootGun(Vector3 direction)
    {
        //Shoot if has more ammo than 0 in mag
        if(currentAmmoInMag > 0)
        {
            if (timeSinceLastShot >= fireRate)
            {
                timeSinceLastShot = 0f;
                currentAmmoInMag -= 1;

                //New shoot
                PooledObject bullet = bulletPool.GetObject();
                bullet.transform.position = this.transform.position;
                bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
                bullet.GetComponent<Bullet>().shooter = this.gameObject;
                bullet.GetComponent<Bullet>().damage = damage;
            }
        }
    }

    private void ReloadGun()
    {
        if(totalAmmo > maxAmmoInMag)
        {
            int ammoToAdd = maxAmmoInMag - currentAmmoInMag;
            totalAmmo -= ammoToAdd;

            currentAmmoInMag = maxAmmoInMag;
        }

        //If total ammo is more than 0, but less than 30(checked by previous if statement)
        else if (totalAmmo > 0)
        {
            int ammoToAdd = maxAmmoInMag - currentAmmoInMag;

            //if the total ammo is more than requested, just fill the mag as usual
            if(totalAmmo >= ammoToAdd)
            {
                currentAmmoInMag = maxAmmoInMag;
                totalAmmo -= ammoToAdd;
            }

            //If total ammo is smaller than ammo to add, add whatever ammo is rest to the magazine
            else if(totalAmmo <= ammoToAdd)
            {
                currentAmmoInMag += totalAmmo;
                totalAmmo = 0;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Item item = collision.collider.GetComponent<Item>();
        if (item != null)
        {
            inventory.PickupItem(item);
        }
    }

    //Get the item from inventory if item selected
    private void Inventory_itemSelected(object sender, InventoryEventsArgs e)
    {
        currentItem = e.Item;
    }
}
