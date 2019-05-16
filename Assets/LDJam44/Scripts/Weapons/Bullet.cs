using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 100;
    public GameObject shooter;
    public float bulletSurviveTime = 10;
    public float currentTime = 0;

    private void Start()
    {
        currentTime = 0;
    }

    private void Update()
    {
        //temporary fix to eventually despawn the bullet
        currentTime += Time.deltaTime;
        if(currentTime > bulletSurviveTime)
        {
            currentTime = 0;
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
                currentTime = 0;
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
