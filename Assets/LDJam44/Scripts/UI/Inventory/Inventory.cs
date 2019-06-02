using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int inventorySlots = 6;
    public List<IInventoryItem> inventoryItems = new List<IInventoryItem>();

    public event EventHandler<InventoryEventsArgs> itemAdded;
    public event EventHandler<InventoryEventsArgs> itemRemoved;
    public event EventHandler<InventoryEventsArgs> itemUsed;
    public event EventHandler<InventoryEventsArgs> itemSelected;

    // Start is called before the first frame update
    public void AddItem(IInventoryItem item)
    {

        if (inventoryItems.Count < inventorySlots)
        {
            Collider2D collider = (item as MonoBehaviour).GetComponent<Collider2D>();
            if (collider.enabled)
            {
                collider.enabled = false;
                inventoryItems.Add(item);
                item.OnPickup();

                if (itemAdded != null)
                {
                    itemAdded(this, new InventoryEventsArgs(item));
                }
            }

            
        }
    }
   

    public void UseItem(IInventoryItem item)
    {
        item.OnUse();
        if (itemUsed != null)
        {
            itemUsed(this, new InventoryEventsArgs(item));
        }
    }

    public void SelectItem(IInventoryItem item)
    {
        
        if (itemSelected != null)
        {
            itemSelected(this, new InventoryEventsArgs(item));
        }
    }

    public void RemoveItem(IInventoryItem item)
    {
        
        if (inventoryItems.Contains(item))
        {
            inventoryItems.Remove(item);

            item.OnDrop();

            Collider2D collider = (item as MonoBehaviour).GetComponent<Collider2D>();
            if (collider != null)
            {
                collider.enabled = true;
            }

            if (itemRemoved != null)
            {
                itemRemoved(this, new InventoryEventsArgs(item));
            }
        }
    }
}
