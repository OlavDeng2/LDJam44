using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class DropHandler : MonoBehaviour, IDropHandler
{
    public InventorySlot invSlot;

    public void OnDrop(PointerEventData eventData)
    {
        DragHandler.targetInvSlot = invSlot;
    }
}
