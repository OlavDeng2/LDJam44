using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    [Header("Health")]
    public float defaultHealth = 100;
    public float health = 100;
    public float damage = 100;
    public bool alive = true;

    [Header("Movement")]
    public float moveSpeed = 1;

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
        if(!alive)
        {
            this.GetComponent<PooledObject>().ReturnToPool();
        }
    }

    private void MoveTowardsPlayer()
    {
        if(player)
        {
            Vector3 directionToPlayer = Vector3.Normalize(player.transform.position - this.gameObject.transform.position);
            MoveCharacter(directionToPlayer);
            LookDirection(directionToPlayer);
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

    //The character should take damage
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0f)
        {
            KillCharacter();
        }
    }

    public void KillCharacter()
    {
        alive = false;
    }

    public void MoveCharacter(Vector3 moveDirection)
    {
        gameObject.transform.position += moveDirection * moveSpeed;
    }

    public void LookDirection(Vector3 lookDirection)
    {
        gameObject.transform.right = lookDirection;
    }
}
