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

    public void OpenTutorial(Player interactingPlayer)
    {
        player = interactingPlayer;
        player.playerUI.InteractWithObjectTalk(talkText);
    }

    public void CloseTutorial()
    {
        player.playerUI.CloseTalk();
    }
}
