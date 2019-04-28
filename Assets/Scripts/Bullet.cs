using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 100;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Character hitCharacter = collision.gameObject.GetComponent<Character>();
        if (hitCharacter)
        {
            hitCharacter.TakeDamage(damage);
        }

        else if (!hitCharacter);
        {
            this.GetComponent<PooledObject>().ReturnToPool();
        }
    }
}
