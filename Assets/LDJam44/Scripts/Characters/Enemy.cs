using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    [Header("Settings")]
    public GameObject[] dropableItems;
    public int maxDroppedItems = 0;
    public int minDroppedItems = 0;
    public float maxDetectionDistance = 10; //distance at which the enemy can detect players

    [Header("Data")]
    public GameObject player;
    public Player playerScript;

    private void Start()
    {
        //This just assumes that there will be only one player
        playerScript = FindObjectOfType<Player>();
        player = playerScript.gameObject;

    }

    public virtual void Update()
    {


        MoveEnemy();

        if (!alive)
        {
            KillCharacter();
        }

    }



    public override void KillCharacter()
    {
        base.KillCharacter();

        //On death, drop the items
        if(dropableItems.Length < 0)
        {
            //get amount of items to drop
            int itemDropCount = Random.Range(minDroppedItems, maxDroppedItems);
            for(int i = 0; i <= itemDropCount; i++)
            {
                GameObject itemToDrop = Instantiate(dropableItems[Random.Range(0, dropableItems.Length)], this.transform.parent);
                itemToDrop.transform.position = this.transform.position;
            }
        }

        //return object to pool
        this.GetComponent<PooledObject>().ReturnToPool();

    }


    public Vector3 GetPlayerDirection()
    {
        if (player)
        {
            Vector3 directionToPlayer = Vector3.Normalize(player.transform.position - this.gameObject.transform.position);
            return directionToPlayer;
        }

        else
        {
            return new Vector3(0, 0, 0);
        }

    }

    public float GetPlayerDistance()
    {
        if (player)
        {
            float distanceToPlayer = Vector3.Magnitude(player.transform.position - this.gameObject.transform.position);
            return distanceToPlayer;
        }

        else
        {
            return 0;
        }

    }

    public virtual void MoveEnemy()
    {
        //TODO: do a raycast to check if enemy sight is being blocked, if not, go get the player
        RaycastHit2D hit = Physics2D.Raycast(transform.position, GetPlayerDirection());

        if (hit.collider != null)
        {
            if (hit.collider.GetComponent<Player>() != null)
            {

                //If the player is further away than max detection distance, dont move
                if (GetPlayerDistance() >= maxDetectionDistance)
                {
                    MoveCharacter(new Vector3(0, 0, 0));
                }

                //Otherwise, move towards the player
                else if (GetPlayerDistance() <= maxDetectionDistance)
                {
                    MoveCharacter(GetPlayerDirection());
                }
            }
        }
    }

}
