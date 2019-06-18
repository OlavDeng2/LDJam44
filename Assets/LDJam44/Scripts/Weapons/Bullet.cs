using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 100;
    public GameObject shooter;
    public Vector3 startPos = new Vector3(0, 0, 0);
    public float maxRange = 10f;

    
    private void Update()
    {

        if(Vector3.Magnitude(startPos - this.transform.position) > maxRange)
        {
            this.GetComponent<PooledObject>().ReturnToPool();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Character hitCharacter = collision.gameObject.GetComponent<Character>();
        if (hitCharacter != null)
        {
            if(collision.gameObject != shooter)
            {
                hitCharacter.TakeDamage(damage);
            }
        }

        if(hitCharacter == null && collision.gameObject.GetComponent<Bullet>() == null)
        {
            this.GetComponent<PooledObject>().ReturnToPool();
        }
    }
}
