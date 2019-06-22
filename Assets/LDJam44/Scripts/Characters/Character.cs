using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("references")]
    public GameObject hand;

    [Header("Health")]
    public float defaultHealth = 100;
    public float health = 100;
    public bool alive = true;

    [Header("Movement")]
    public float moveSpeed = 1;
    public Rigidbody2D rigidBody;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip[] randomClips;
    public AudioClip[] dyingClips;

    [Header("Inventory")]
    public Inventory inventory;

    [Header("Data")]
    public GameManager gameManager;

    //The character should take damage
    public void TakeDamage(float damage)
    {
        health -= damage;
        if( health <= 0f)
        {
            KillCharacter();
        }
    }

    public virtual void KillCharacter()
    {
        alive = false;

        if (dyingClips.Length > 0)
        {
            audioSource.PlayOneShot(dyingClips[Random.Range(0, dyingClips.Length)]);
        }
    }

    public void MoveCharacter(Vector3 moveDirection)
    {
        rigidBody.velocity = moveDirection * moveSpeed;
    }

    public void RandomClip(float chance)
    {
        float random = Random.Range(0, 1);
        if(chance > random)
        {
            if (randomClips.Length > 0)
            {
                audioSource.PlayOneShot(randomClips[Random.Range(0, randomClips.Length)]);
            }
        }
    }
}
