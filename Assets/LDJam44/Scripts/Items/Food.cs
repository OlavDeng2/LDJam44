using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour, IInventoryItem
{
    public string Name
    {
        get
        {
            return "Food";
        }
    }

    public Sprite image = null;

    public Sprite Image
    {
        get
        {
            return image;
        }
    }

    public void OnPickup()
    {
        //Do something when picked up
    }

    public void OnUse()
    {
        //Do something when used
    }
}
