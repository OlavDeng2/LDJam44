using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bandit : Enemy
{
    [Header("Gun")]
    public Weapon gun;
    public ObjectPool bulletPool;
    public float fireRange = 8;

    // Start is called before the first frame update
    void Start()
    {

        bulletPool = GameObject.Find("Pool: Bullets").GetComponent<ObjectPool>() ;
    }

    // Update is called once per frame
    public override void Update()
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

            if(gun.currentAmmo > 0)
            {
                gun.UseItem();
                gun.StartCycleBolt();
            }

            else if(gun.currentAmmo <= 0)
            {
                gun.StartReload();
            }
        }
    }

}
