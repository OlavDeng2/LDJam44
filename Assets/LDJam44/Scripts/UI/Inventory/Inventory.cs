using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int slots = 6;
    public List<IInventoryItem> mItems = new List<IInventoryItem>();
    public event EventHandler<InventoryEventsArgs> itemAdded;
    public event EventHandler<InventoryEventsArgs> itemRemoved;
    public event EventHandler<InventoryEventsArgs> itemUsed;

    // Start is called before the first frame update
    public void AddItem(IInventoryItem item)
    {
        if (mItems.Count < slots)
        {
            Collider2D collider = (item as MonoBehaviour).GetComponent<Collider2D>();
            if (collider.enabled)
            {
                collider.enabled = false;
                mItems.Add(item);
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

    public void RemoveItem(IInventoryItem item)
    {
        if (mItems.Contains(item))
        {
            mItems.Remove(item);

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
