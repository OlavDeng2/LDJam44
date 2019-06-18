using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bandit : Enemy
{
    [Header("Gun")]
    public ObjectPool bulletPool;
    public float fireRange = 8;

    // Start is called before the first frame update
    void Start()
    {

        bulletPool = GameObject.Find("Pool: Bullets").GetComponent<ObjectPool>() ;
    }

    // Update is called once per frame
    void Update()
    {
        if(GetPlayerDistance() >= fireRange)
        {
            MoveCharacter(GetPlayerDirection());
        }

        if (!alive)
        {
            KillCharacter();
        }


        if (GetPlayerDistance() <= fireRange)
        {

            //ShootGun(GetPlayerDirection());

        }
    }

}
