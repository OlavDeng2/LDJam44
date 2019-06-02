using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Inventory inventory;
    public Item item;
    public Image itemImage;

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
        if(Input.GetKeyDown(key))
        {
            inventory.SelectItem(item);
        }
    }

    public void AddItem(Item itemToAdd)
    {
        item = itemToAdd;
        if(!itemImage.enabled)
        {
            itemImage.enabled = true;
            itemImage.sprite = item.image;
        }
    }

    public void RemoveItem()
    {
        item = null;
        if(itemImage.enabled)
        {
            itemImage.sprite = null;
            itemImage.enabled = false;
        }
    }
}
