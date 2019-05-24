using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : Interactable
{
    public override void Interact(Player interactingPlayer)
    {
        base.Interact(interactingPlayer);

        interactingPlayer.playerUI.GameWin();
    }
}
