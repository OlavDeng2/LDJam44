using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("Health")]
    public float defaultHealth = 100;
    public float health = 100;
    public float damage = 100;
    public bool alive = true;

    [Header("Movement")]
    public float moveSpeed = 1;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip[] randomClips;
    public AudioClip[] dyingClips;

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

        if (dyingClips.Length < 0)
        {
            audioSource.PlayOneShot(dyingClips[Random.Range(0, dyingClips.Length)]);
        }
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
