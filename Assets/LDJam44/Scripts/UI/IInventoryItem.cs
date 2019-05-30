﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventoryItem
{
    string Name { get; }
    Sprite Image { get;  }

    void OnPickup();
    void OnUse();
}

public class InventoryEventsArgs : EventArgs
{
    public InventoryEventsArgs(IInventoryItem item)
    {
        Item = item;
    }

    public IInventoryItem Item;

}
