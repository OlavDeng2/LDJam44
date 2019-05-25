using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : Interactable
{
    public override void Interact()
    {
        base.Interact();

        player.playerUI.GameWin();
    }
}
