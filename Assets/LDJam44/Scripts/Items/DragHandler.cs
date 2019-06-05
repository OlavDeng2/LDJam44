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
    public InventoryItem item;
    public int itemAmount;

    //Image to drag
    public GameObject image;
    Vector3 startPos;

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
        canvasGroup.blocksRaycasts = false;
        invSlot = gameObject.GetComponent<InventorySlot>();
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
        //DragHandler.targetInvSlot = this.GetComponent<InventorySlot>();
        Debug.Log(targetInvSlot);

        if(targetInvSlot == invSlot || targetInvSlot == null)
        {
            image.transform.position = startPos;
        }

        
        else if(targetInvSlot != invSlot)
        {
            targetInvSlot.AddItem(item, itemAmount);
            image.transform.position = startPos;
            invSlot.RemoveItem();
        }

        //reset data
        item = null;
        itemAmount = 0;
        image = null;
        canvasGroup.blocksRaycasts = true;

    }
}
