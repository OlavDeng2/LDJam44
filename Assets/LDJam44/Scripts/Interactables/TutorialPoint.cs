using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPoint : MonoBehaviour
{
    [Header("Settings")]
    public bool hasBeenOpened = false;
    public string talkText = "Tutorial";

    [Header("Data")]
    public Player player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!hasBeenOpened)
        {
            player = collision.GetComponent<Player>();
            if (player)
            {
                player.tutorial = this;
                player.playerUI.InteractWithObjectTalk(talkText);
                hasBeenOpened = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (player)
        {
            player.tutorial = null;
            player = null;
            this.gameObject.SetActive(false);
        }
    }
}
