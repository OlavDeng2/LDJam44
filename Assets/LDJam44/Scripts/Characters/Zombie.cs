using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Enemy
{
    // Update is called once per frame
    void Update()
    {
        MoveTowardsPlayer();
        if(!alive)
        {
            KillCharacter();
        }
    }

    private void MoveTowardsPlayer()
    {

        MoveCharacter(GetPlayerDirection());

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if it is the player, deal damage to it
        if(collision.gameObject == player)
        {
            playerScript.TakeDamage(damage);
        }
    }
}
