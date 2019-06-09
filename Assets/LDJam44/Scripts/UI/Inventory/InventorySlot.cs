using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [Header("Settings")]
    public KeyCode key;


    [Header("References")]
    public Inventory inventory;
    public Image itemImage;

    [Header("Data")]
    public InventoryItem item;
    public int amount = 0;

    public void Start()
    {
        inventory = FindObjectOfType<Inventory>();        
    }

    private void Update()
    {
        //only select the item when the appropriate button is pressed
        if (Input.GetKeyDown(key))
        {
            inventory.SelectItem(item, this);
        }
    }

    public void AddItem(InventoryItem itemToAdd, int amountToAdd)
    {
        item = itemToAdd;
        amount += amountToAdd;
        if(!itemImage.enabled)
        {
            itemImage.enabled = true;
            itemImage.sprite = item.image;
            
        }
    }

    public void RemoveItem()
    {
        item = null;
        amount = 0;
        if(itemImage.enabled)
        {
            itemImage.sprite = null;
            itemImage.enabled = false;
        }
    }
}
