using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int slots = 6;
    private List<IInventoryItem> mItems = new List<IInventoryItem>();
    public event EventHandler<InventoryEventsArgs> itemAdded;

    // Start is called before the first frame update
    public void AddItem(IInventoryItem item)
    {
        if (mItems.Count < slots)
        {
            Collider collider = (item as MonoBehaviour).GetComponent<Collider>();
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
}
