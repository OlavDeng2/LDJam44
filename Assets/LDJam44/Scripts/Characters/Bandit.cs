﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bandit : Enemy
{
    [Header("Gun")]
    public Weapon gun;
    public float fireRange = 8;

    public override void Start()
    {
        GetSpawnWeapon();

        base.Start();
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

        if (gun != null)
        {
            if (GetPlayerDistance() <= fireRange)
            {

                if (gun.currentAmmo > 0)
                {
                    gun.UseItem();
                    gun.StartCycleBolt();
                }

                else if (gun.currentAmmo <= 0)
                {
                    gun.StartReload();
                }
            }
        }
        
    }

    public void GetSpawnWeapon()
    {
        foreach(InventorySlot invSlot in inventory.inventorySlots)
        {
            Weapon weapon = invSlot.item.GetComponent<Weapon>();
            if(weapon != null)
            {
                gun = weapon;
                invSlot.item.SetActive(true);
                break;
            }
        }
    }

}
