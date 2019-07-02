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
        //return item back to orignal
        if(targetInvSlot == invSlot)
        {
            image.transform.position = startPos;
        }

        //move the item
        else if(targetInvSlot != invSlot && targetInvSlot != null)
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
        }

        //Drop the item
        else if (targetInvSlot == null)
        {

            //set location + 10 on the z axis so it isnt hidden from the camera
            invSlot.item.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3 (0, 0, 10);
            invSlot.item.gameObject.SetActive(true);
            invSlot.item.GetComponent<Collider2D>().enabled = true;
            invSlot.item.transform.parent = null;
            invSlot.RemoveItem();
            image.transform.position = startPos;
        }

        //reset data
        item = null;
        itemAmount = 0;
        image = null;
        targetInvSlot = null;
        canvasGroup.blocksRaycasts = true;
    }
}
