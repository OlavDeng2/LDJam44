using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("Data")]
    public InventorySlot[] inventorySlots;
    public event EventHandler<InventoryEventsArgs> itemSelected;

    public void Start()
    {
        inventorySlots = GetComponentsInChildren<InventorySlot>(true);
    }


    public void PickupItem(InventoryItem item,GameObject gameObjectItem, int itemAmount)
    {

        foreach (InventorySlot inventorySlot in inventorySlots)
        {
            if(inventorySlot.item != null)
            {
                if(item == inventorySlot.item && (inventorySlot.amount + itemAmount) <= inventorySlot.item.maxStackCount)
                {
                    //Debug.Log("item in slot: " + inventorySlot.item.name);
                    inventorySlot.amount += itemAmount;
                    break;
                }
            }

            else if(inventorySlot.item == null)
            {
                inventorySlot.AddItem(item, gameObjectItem, itemAmount);
                break;
            }
        }
    }

    public void MoveItem(InventorySlot initialSlot, InventorySlot targetSlot)
    {
        if (targetSlot.item != null)
        {
            if (initialSlot.item.name == targetSlot.item.name && targetSlot.amount <= targetSlot.item.maxStackCount)
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

    public void SelectItem(InventoryItem item, InventorySlot invSlot)
    {
        if (itemSelected != null)
        {
            itemSelected(this, new InventoryEventsArgs(item, invSlot));
        }
    }
}
