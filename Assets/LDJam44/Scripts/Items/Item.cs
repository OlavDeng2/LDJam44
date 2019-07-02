using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is the monobehaviour for the item in the world, all it does is interact with the inventory item scriptable object
public class Item : MonoBehaviour
{
    [Header("Settings")]
    public Sprite image = null;
    public float timeBetweenUses = 1f;
    public int amount = 1; //Only for items on the ground
    public int maxStackCount = 1;
    public int buyPrice = 20; //Price the player buys the item for
    public int sellPrice = 10; //Price the player sells the item for

    [Header("Data")]
    public float currentTime = 0f;
    public Character character;
    public InventorySlot invSlot;
    public bool canUseItem = true;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip[] useItemAudioClips;


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

            if (useItemAudioClips.Length > 0)
            {
                audioSource.PlayOneShot(useItemAudioClips[UnityEngine.Random.Range(0, useItemAudioClips.Length)]);
            }
        }
    }
}

public class InventoryEventsArgs : EventArgs
{
    public InventoryEventsArgs(GameObject item, InventorySlot invSlot)
    {
        Item = item;
        InvSlot = invSlot;
    }

    public GameObject Item;
    public InventorySlot InvSlot;
    
}
