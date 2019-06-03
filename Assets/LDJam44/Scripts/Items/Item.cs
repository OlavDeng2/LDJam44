using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is the monobehaviour for the item in the world, all it does is interact with the inventory item scriptable object
public class Item : MonoBehaviour
{
    public int amount = 1;
    public Sprite image = null;

    public InventoryItem inventoryItem;

    public virtual void UseItem()
    {
        throw new System.NotImplementedException();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameObject.SetActive(false);
    }
}

public class InventoryEventsArgs : EventArgs
{
    public InventoryEventsArgs(InventoryItem item)
    {
        Item = item;
    }

    public InventoryItem Item;
    
}
