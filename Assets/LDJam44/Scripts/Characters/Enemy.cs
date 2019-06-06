using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    [Header("Settings")]
    public InventoryItem[] dropableItems;
    public int maxDroppedItems = 0;
    public int minDroppedItems = 0;
    
    public override void KillCharacter()
    {
        base.KillCharacter();

        //On death, drop the items
        if(dropableItems.Length<= 0)
        {
            //get amount of items to drop
            int itemDropCount = Random.Range(minDroppedItems, maxDroppedItems);
            for(int i = 0; i <= itemDropCount; i++)
            {
                InventoryItem itemToDrop = dropableItems[Random.Range(0, dropableItems.Length)];
            }
        }

        //return object to pool
        this.GetComponent<PooledObject>().ReturnToPool();

    }

}
