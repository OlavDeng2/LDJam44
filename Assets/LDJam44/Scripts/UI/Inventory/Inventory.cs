using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public InventorySlot[,] inventorySlots = new InventorySlot[6, 4];

    public event EventHandler<InventoryEventsArgs> inventoryUpdated;
    public event EventHandler<InventoryEventsArgs> itemSelected;

    public void PickupItem(InventoryItem item)
    {
        //loop through all the slots
        //for (int x = 0; x < inventoryItems.GetLength(0); x++)
        //{
        //    for (int y = 0; y < inventoryItems.GetLength(1); y++)
        //    {
        //        //check if item is already in inventory
        //        if (inventoryItems[x, y] != null)
        //        {
        //            if (inventoryItems[x, y].name == item.name)
        //            {
        //                inventoryItems[x, y].amount += item.amount;
        //                item.OnPickup();

        //                if (inventoryUpdated != null)
        //                {
        //                    inventoryUpdated(this, new InventoryEventsArgs(item));
        //                    break;
        //                }
        //            }
        //        }
        //        //otherwise, find an empty slot
        //        if (inventoryItems[x, y] == null)
        //        {
        //            inventoryItems[x, y] = item;
        //            item.OnPickup();

        //            if (inventoryUpdated != null)
        //            {
        //                inventoryUpdated(this, new InventoryEventsArgs(item));
        //                break;

        //            }
        //        }
        //    } 
        //}
    }

    public void AddItem(InventoryItem item, int x, int y)
    {
        //if (inventoryItems[x, y] != null)
        //{
        //    if (inventoryItems[x, y].name == item.name)
        //    {
        //        inventoryItems[x, y].amount += item.amount;
        //    }
        //}

        //else if (inventoryItems[x, y] == null)
        //{
        //    inventoryItems[x, y] = item;
        //}

        //if (inventoryUpdated != null)
        //{
        //    inventoryUpdated(this, new InventoryEventsArgs(item));
        //}
    }

    //public InventoryItem RemoveItem(int x, int y)
    //{
        //Item item = inventoryItems[x, y];
        //inventoryItems[x, y] = null;
        //if(inventoryUpdated!= null)
        //{
        //    inventoryUpdated(this, new InventoryEventsArgs(item));
        //}


        //return item;
    //}

    //public void SelectItem(InventoryItem item)
    //{
    //    if (itemSelected != null)
    //    {
    //        itemSelected(this, new InventoryEventsArgs(item));
    //    }
    //}
}
