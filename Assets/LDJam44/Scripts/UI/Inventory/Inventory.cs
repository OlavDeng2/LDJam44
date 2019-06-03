using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public InventorySlot[] inventorySlots;

    public event EventHandler<InventoryEventsArgs> itemSelected;

    public void Start()
    {
        inventorySlots = GetComponentsInChildren<InventorySlot>(true);
    }


    public void PickupItem(InventoryItem item)
    {
        foreach(InventorySlot inventorySlot in inventorySlots)
        {
            if(inventorySlot.item != null)
            {
                if(item.name == inventorySlot.item.name)
                {
                    inventorySlot.amount += 1;
                    break;
                }
            }

            else if(inventorySlot.item == null)
            {
                inventorySlot.AddItem(item);
                break;
            }
        }
    }

    public void MoveItem(InventorySlot initialSlot, InventorySlot targetSlot)
    {
        if (targetSlot.item != null)
        {
            if (initialSlot.item.name == targetSlot.item.name)
            {
                targetSlot.amount += initialSlot.amount;
                initialSlot.RemoveItem();
            }
        }
        
        else if(targetSlot.item == null)
        {
            targetSlot.item = initialSlot.item;
            targetSlot.amount = initialSlot.amount;
            initialSlot.RemoveItem();
        }
    }

    public InventoryItem RemoveItem(int slot)
    {
        return inventorySlots[slot].item;
    }

    public void SelectItem(InventoryItem item)
    {
        if (itemSelected != null)
        {
            itemSelected(this, new InventoryEventsArgs(item));
        }
    }
}
