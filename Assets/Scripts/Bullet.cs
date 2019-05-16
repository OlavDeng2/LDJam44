using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 100;
    public GameObject shooter;
    public float bulletSpeed = 1;
    public Vector3 moveDirection;
    public List<Collider2D> collisions;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        collisions.Add( collision);
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collisions.Contains(collision))
        {
            collisions.Remove(collision);
        }
    }
}
