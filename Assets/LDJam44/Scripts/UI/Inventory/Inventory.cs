using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("Data")]
    public InventorySlot[] inventorySlots;
    
    //Events
    public event EventHandler<InventoryEventsArgs> itemSelected;
    public event EventHandler<InventoryEventsArgs> itemAdded;
    public event EventHandler<InventoryEventsArgs> itemRemoved;




    public void Awake()
    {
        inventorySlots = GetComponentsInChildren<InventorySlot>(true);

        foreach(InventorySlot invSlot in inventorySlots)
        {
            invSlot.inventory = this;
        }
    }


    public void PickupItem(GameObject item, int itemAmount)
    {
        
        foreach (InventorySlot inventorySlot in inventorySlots)
        {
            if(inventorySlot.item != null)
            {
                if(item == inventorySlot.item && (inventorySlot.amount + itemAmount) <= inventorySlot.item.GetComponent<Item>().maxStackCount)
                {
                    //Debug.Log("item in slot: " + inventorySlot.item.name);
                    inventorySlot.amount += itemAmount;
                    break;
                }
            }

            else if(inventorySlot.item == null)
            {
                inventorySlot.AddItem(item, itemAmount);
                break;
            }
        }
    }

    public void MoveItem(InventorySlot initialSlot, InventorySlot targetSlot)
    {
        if (targetSlot.item != null)
        {
            if (initialSlot.item.name == targetSlot.item.name && targetSlot.amount <= targetSlot.item.GetComponent<Item>().maxStackCount)
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

    public void SelectItem(GameObject item, InventorySlot invSlot)
    {
        if (itemSelected != null)
        {
            itemSelected(this, new InventoryEventsArgs(item, invSlot));
        }
    }

    public void ItemAdded(GameObject item, InventorySlot invSlot)
    {
        if (itemAdded != null)
        {
            itemAdded(this, new InventoryEventsArgs(item, invSlot));
        }
    }

    public void ItemRemoved(GameObject item, InventorySlot invSlot)
    {
        if (itemRemoved != null)
        {
            itemRemoved(this, new InventoryEventsArgs(item, invSlot));
        }
    }

    public void ClearInventory()
    {
        foreach(InventorySlot invslot in inventorySlots)
        {
            if(invslot.item != null)
            {
                Debug.Log(invslot.item);
                invslot.RemoveItem();
            }
        }
    }
}
