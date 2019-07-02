using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [Header("Data")]
    public Player player;
    public string talkText = "interactable";
    public bool playerHasTalked = false;


    public void tryToInteract()
    {
        if (!playerHasTalked)
        {
            talk();
        }
        else if (playerHasTalked)
        {
            Interact();
        }
    }
    public virtual void Interact()
    {
        player.playerUI.CloseTalk();
        playerHasTalked = false;
        //doSomething

    }
    public void talk()
    {
        player.playerUI.InteractWithObjectTalk(talkText);
        playerHasTalked = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.GetComponent<Player>();
        if(player)
        {
            player.interactable = this;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(player)
        {
            //if (collision.gameObject == player.gameObject)
            {
                player.interactable = null;
                player = null;
                playerHasTalked = false;
            }
        }
        
    }
}
