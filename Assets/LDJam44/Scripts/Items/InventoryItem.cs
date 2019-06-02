using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "item", menuName ="items/item")]
public class InventoryItem : ScriptableObject
{
    public GameObject itemPrefab = null;
    public string name = "Item";
    public int amount = 1;
    public Sprite image = null;

    public void OnDrop()
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //gameObject.SetActive(true);
        //gameObject.transform.position = worldPoint;

        Debug.Log("dropping item");
    }

    public void OnPickup()
    {
        Debug.Log("item was picked up");
        //gameObject.SetActive(false);
    }

    public virtual void UseItem()
    {
        throw new System.NotImplementedException();
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