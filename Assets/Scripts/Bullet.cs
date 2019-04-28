using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 100;
    public GameObject shooter;
    public AudioClip shootSound;
    public AudioSource source;

    private void Start()
    {
        source.PlayOneShot(shootSound);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Character hitCharacter = collision.gameObject.GetComponent<Character>();
        if (hitCharacter)
        {
            if(collision.gameObject != shooter)
            {
                hitCharacter.TakeDamage(damage);
                this.GetComponent<PooledObject>().ReturnToPool();
            }
        }

        /*
        else if (!hitCharacter);
        {
            Debug.Log("hit");

            this.GetComponent<PooledObject>().ReturnToPool();
        }*/
    }
}
