using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("References")]
    public InventorySlot invSlot;
    public CanvasGroup canvasGroup;

    [Header("Data")]
    //Inventory specific stuff
    public static InventorySlot targetInvSlot;
    public GameObject item;
    public int itemAmount;
    //Image to drag
    public GameObject image;
    Vector3 startPos;

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        item = invSlot.item;

        itemAmount = invSlot.amount;
        image = invSlot.itemImage.gameObject;
        startPos = image.transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        image.transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(targetInvSlot == invSlot || targetInvSlot == null)
        {
            image.transform.position = startPos;
        }

        
        else if(targetInvSlot != invSlot)
        {
            if (targetInvSlot.item == null)
            {
                targetInvSlot.AddItem(item, itemAmount);
                image.transform.position = startPos;
                invSlot.RemoveItem();
            }

            else if (targetInvSlot.item == this.item && (targetInvSlot.amount + itemAmount) <= targetInvSlot.item.GetComponent<Item>().maxStackCount)
            {
                targetInvSlot.AddItem(item, (targetInvSlot.amount + itemAmount));
                image.transform.position = startPos;
                invSlot.RemoveItem();
            }
            else;
            {
                image.transform.position = startPos;
            }

        }

        //reset data
        item = null;
        itemAmount = 0;
        image = null;
        canvasGroup.blocksRaycasts = true;
    }
}
