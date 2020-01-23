using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    [Header("Settings")]
    public GameObject[] dropableItems;
    public int maxItems = 0;
    public int minItems = 0;
    public float maxDetectionDistance = 10; //distance at which the enemy can detect players

    [Header("Data")]
    public GameObject player;
    public Player playerScript;

    public virtual void Start()
    {
        //This just assumes that there will be only one player
        playerScript = FindObjectOfType<Player>();
        player = playerScript.gameObject;

        //Get the amount of items to drop
        int itemCount = Random.Range(minItems, maxItems);
        
        if(itemCount >= inventory.inventorySlots.Length)
        {
            //Set to max just incase there are not enough inventory slots
            itemCount = inventory.inventorySlots.Length;

            
        }

        // loop as many times as there are items to add
        for (int i = 0; i < itemCount; i++)
        {
            GameObject itemToAddToInv = Instantiate(dropableItems[Random.Range(0, dropableItems.Length)], this.transform);
            itemToAddToInv.transform.position = this.transform.position;

            if (inventory.inventorySlots[i].item == null)
            {
                //Get random amount to add to the inventory slot between 1 and max stack amount (so should always be 1 if max stack amount is 1)
                inventory.inventorySlots[i].AddItem(itemToAddToInv, UnityEngine.Random.Range(1, itemToAddToInv.GetComponent<Item>().maxStackCount));
                itemToAddToInv.SetActive(false);
            }

            else if (inventory.inventorySlots[i].item != null)
            {
                break;
            }
        }
    }


    public virtual void Update()
    {
        if (!gameManager.isPaused && !gameManager.gameOver)
        {
            MoveCharacter(GetPlayerDirection());
        }

        if (!alive)
        {
            KillCharacter();
        }

    }



    public override void KillCharacter()
    {
        base.KillCharacter();

        foreach (InventorySlot invSlot in inventory.inventorySlots)
        {
            if(invSlot.item != null)
            {
                invSlot.item.SetActive(true);
                invSlot.item.GetComponent<Collider2D>().enabled = true;
                invSlot.item.GetComponent<Item>().amount = invSlot.amount;
                invSlot.item.transform.parent = null;
                invSlot.RemoveItem();
            }
        }
        
        //return object to pool
        this.GetComponent<PooledObject>().ReturnToPool();

        //reset the pooled object to default
        health = defaultHealth;
        

    }


    public Vector3 GetPlayerDirection()
    {
        if (player)
        {
            Vector3 directionToPlayer = Vector3.Normalize(player.transform.position - this.gameObject.transform.position);
            return directionToPlayer;
        }

        else
        {
            return new Vector3(0, 0, 0);
        }

    }

    public float GetPlayerDistance()
    {
        if (player)
        {
            float distanceToPlayer = Vector3.Magnitude(player.transform.position - this.gameObject.transform.position);
            return distanceToPlayer;
        }

        else
        {
            return 0;
        }

    }

}
