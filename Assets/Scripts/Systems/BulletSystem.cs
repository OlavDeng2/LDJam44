using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class BulletSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((Bullet bullet) => {
            Debug.Log("It is working");
        });
    }
}
