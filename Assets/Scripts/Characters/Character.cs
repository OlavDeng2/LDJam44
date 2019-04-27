using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("Health")]
    public float health = 100;
    public float damage = 100;
    public bool alive = true;

    [Header("Movement")]
    public float moveSpeed = 1;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //The character should take damage
    public void TakeDamage(float damage)
    {
        health -= damage;
        if( health <= 0f)
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
