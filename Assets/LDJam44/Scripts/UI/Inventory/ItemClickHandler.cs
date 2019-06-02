using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemClickHandler : MonoBehaviour
{
    public Inventory inventory;

    public KeyCode key;
    private Button button;

    public void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        button = GetComponent<Button>();
    }

    private void Update()
    {
        //only select the item when the appropriate button is pressed
        if(Input.GetKeyDown(key))
        {
            inventory.SelectItem(GetItem());
        }
    }

    //Get the item so we can drag it when the button is clicked
    public void OnItemClicked()
    {
        GetItem();
    }

    private IInventoryItem GetItem()
    {
        ItemDragHandler dragHandler = gameObject.transform.Find("ItemImage").GetComponent<ItemDragHandler>();
        return dragHandler.item;

    }
}
