using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is the monobehaviour for the item in the world, all it does is interact with the inventory item scriptable object
public class Item : MonoBehaviour
{
    public InventoryItem inventoryItem = null;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        inventoryItem.OnPickup();
        gameObject.SetActive(false);
    }
}
