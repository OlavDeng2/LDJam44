using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [Header("Data")]
    public Player player;

    public virtual void Interact(Player interactingPlayer)
    {
        //do something
        interactingPlayer = player;
    }
}
