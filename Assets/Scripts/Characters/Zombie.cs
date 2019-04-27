﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Character
{
    public GameObject player;
    public Player playerScript;
    // Start is called before the first frame update
    void Start()
    {
        //This just assumes that there will be only one player
        playerScript = FindObjectOfType<Player>();
        player = playerScript.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        MoveTowardsPlayer();
    }

    private void MoveTowardsPlayer()
    {
        if(player)
        {
            MoveCharacter(Vector3.Normalize(player.transform.position - this.gameObject.transform.position));
        }
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
