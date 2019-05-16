using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class BulletSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((Bullet bullet) => {
            foreach(Collider2D col in bullet.collisions)
            {
                Character hitCharacter = col.gameObject.GetComponent<Character>();
                if (hitCharacter)
                {
                    if (col.gameObject != bullet.shooter)
                    {
                        hitCharacter.TakeDamage(bullet.damage);
                        UnityEngine.Object.Destroy(bullet.gameObject);
                    }
                }

                /*
                else if (!hitCharacter);
                {
                    Debug.Log("hit");

                    this.GetComponent<PooledObject>().ReturnToPool();
                }*/
            }


        });
    }
}
