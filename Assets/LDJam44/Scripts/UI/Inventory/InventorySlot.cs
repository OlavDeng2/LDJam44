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
    public Text amountText;
    public Text priceText;

    [Header("Data")]
    public GameObject item;
    public int amount = 0;

    private void Update()
    {
        //only select the item when the appropriate button is pressed
        if (Input.GetKeyDown(key))
        {
            inventory.SelectItem(item, this);
        }
    }

    public void AddItem(GameObject itemToAdd, int amountToAdd)
    {
        item = itemToAdd;
        //item.gameObject.transform.SetParent(this.gameObject.transform);
        item.GetComponent<Item>().amount = amountToAdd;
        amount += amountToAdd;

        if(itemImage != null)
        {
            if (!itemImage.enabled)
            {
                itemImage.enabled = true;
                itemImage.sprite = item.GetComponent<Item>().image;
            }
        }

        if(amountText != null)
        {
            amountText.enabled = true;
            amountText.text = amount.ToString();
        }

        inventory.ItemAdded(item, this);
    }

    public void RemoveItem()
    {
        inventory.ItemRemoved(item, this);

        item = null;
        amount = 0;
        if(itemImage != null)
        {
            if (itemImage.enabled)
            {
                itemImage.sprite = null;
                itemImage.enabled = false;
            }
        }

        if(amountText != null)
        {
            amountText.enabled = false;
            amountText.text = amount.ToString();
        }
    }
}
