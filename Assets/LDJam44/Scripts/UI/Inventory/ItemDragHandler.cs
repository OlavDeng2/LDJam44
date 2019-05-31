using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour, IDragHandler, IEndDragHandler
{

    public IInventoryItem item;
    public Inventory inventory;

    private void Start()
    {
        inventory = FindObjectOfType<Inventory>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        //TODO: logic for if the item is trying to be dropped on to another slot, if not, drop the item infront of the player at a certain distance away
        transform.localPosition = Vector3.zero;
        inventory.RemoveItem(item);

    }
}
