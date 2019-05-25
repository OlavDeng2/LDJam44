using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [Header("Data")]
    public Player player;
    public string talkText = "interactable";
    public bool playerHasTalked = false;


    public void tryToInteract(Player interactingPlayer)
    {
        //do something
        interactingPlayer = player;

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
        //doSomething

    }
    public void talk()
    {
        player.playerUI.InteractWithObjectTalk(talkText);
        playerHasTalked = true;
    }
}
