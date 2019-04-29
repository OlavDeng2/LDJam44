using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 100;
    public GameObject shooter;
    public float bulletSurviveTime = 10;
    public float currentTime = 0;
    public AudioClip shootSound;
    public AudioSource source;

    private void Start()
    {
        source.PlayOneShot(shootSound);
    }

    private void Update()
    {
        //temporary fix to eventually despawn the bullet
        currentTime += Time.deltaTime;
        if(currentTime > bulletSurviveTime)
        {
            this.GetComponent<PooledObject>().ReturnToPool();
        }
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
