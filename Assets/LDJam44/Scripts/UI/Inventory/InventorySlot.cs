using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Inventory inventory;
    public InventoryItem item;
    public Image itemImage;
    public int amount = 0;

    public KeyCode key;
    private Button button;

    public void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        button = GetComponentInChildren<Button>();
        
    }

    private void Update()
    {
        //only select the item when the appropriate button is pressed
        if (Input.GetKeyDown(key))
        {
            inventory.SelectItem(item);
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
