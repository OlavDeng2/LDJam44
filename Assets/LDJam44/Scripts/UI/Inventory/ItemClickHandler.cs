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
        if(Input.GetKeyDown(key))
        {
            button.onClick.Invoke();
        }
    }
    // Start is called before the first frame update
    public void OnItemClicked()
    {
        ItemDragHandler dragHandler = gameObject.transform.Find("ItemImage").GetComponent<ItemDragHandler>();
        IInventoryItem item = dragHandler.item;

        inventory.SelectItem(item);
    }
}
