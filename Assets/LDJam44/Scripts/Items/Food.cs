using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : Item
{

    [Header("Settings")]
    public int healAmount = 10;

    public override void UseItem()
    {
        if(canUseItem)
        {
            player.health += healAmount;

            //Decrease by 1
            invSlot.amount -= 1;

            if (invSlot.amount <= 0)
            {
                invSlot.RemoveItem();
                Destroy(this.gameObject);
            }

            if (useItemAudioClip != null)
            {
                audioSource.PlayOneShot(useItemAudioClip);
            }

            canUseItem = false;
        }
        
    }
}
