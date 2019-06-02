using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string name = "Item";
    public int amount = 1;
    public Sprite image = null;

    public void OnDrop()
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        gameObject.SetActive(true);
        gameObject.transform.position = worldPoint;

        Debug.Log("dropping item");
    }

    public void OnPickup()
    {
        gameObject.SetActive(false);
    }

    public virtual void OnUse()
    {
        throw new System.NotImplementedException();
    }

    
}

public class InventoryEventsArgs : EventArgs
{
    public InventoryEventsArgs(Item item)
    {
        Item = item;
    }

    public Item Item;

}