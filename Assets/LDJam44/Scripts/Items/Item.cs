using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is the monobehaviour for the item in the world, all it does is interact with the inventory item scriptable object
public class Item : MonoBehaviour
{
    [Header("Settings")]
    public Sprite image = null;
    public InventoryItem inventoryItem;
    public float timeBetweenUses = 1f;
    public int amount = 1; //Only for items on the ground

    [Header("Data")]
    public float currentTime = 0f;
    public Player player;
    public InventorySlot invSlot;
    public bool canUseItem = true;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip useItemAudioClip;


    public virtual void Update()
    {
       if(!canUseItem)
        {
            currentTime += Time.deltaTime;
            if(currentTime > timeBetweenUses)
            {
                currentTime = 0;
                canUseItem = true;
            }
        }
    }

    public virtual void UseItem()
    {
        if(canUseItem)
        {
            canUseItem = false;

            if(useItemAudioClip != null)
            {
                audioSource.PlayOneShot(useItemAudioClip);
            }
            
        }

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameObject.SetActive(false);
    }
}

public class InventoryEventsArgs : EventArgs
{
    public InventoryEventsArgs(InventoryItem item, InventorySlot invSlot)
    {
        Item = item;
        InvSlot = invSlot;
    }

    public InventoryItem Item;
    public InventorySlot InvSlot;
    
}
