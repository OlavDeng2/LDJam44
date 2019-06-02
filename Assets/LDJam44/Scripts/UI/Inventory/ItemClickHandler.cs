using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemClickHandler : MonoBehaviour
{
    public Inventory inventory;
    public void Start()
    {
        inventory = FindObjectOfType<Inventory>();
    }
    // Start is called before the first frame update
    public void OnItemClicked()
    {
        ItemDragHandler dragHandler = gameObject.transform.Find("ItemImage").GetComponent<ItemDragHandler>();
        IInventoryItem item = dragHandler.item;

        inventory.SelectItem(item);
    }
}
