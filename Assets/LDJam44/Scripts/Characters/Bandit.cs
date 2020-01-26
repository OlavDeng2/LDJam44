﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bandit : Enemy
{
    [Header("Gun")]
    public Weapon gun;
    public float fireRange = 8;

    public override void OnEnable()
    {
        base.OnEnable();

        GetSpawnWeapon();

        if(gun != null)
        {
            gun.StartReload();

        }
    }

    // Update is called once per frame
    public override void Update()
    {
        if (!gameManager.isPaused && !gameManager.gameOver)
        {
            if (GetPlayerDistance() >= fireRange)
            {
                MoveCharacter(GetPlayerDirection());
            }

            if (gun != null)
            {
                if (GetPlayerDistance() <= fireRange)
                {

                    if (gun.currentAmmo > 0)
                    {
                        gun.UseItem();
                        gun.aimDirection = GetPlayerDirection();
                        gun.StartCycleBolt();
                    }

                    else if (gun.currentAmmo <= 0)
                    {
                        gun.StartReload();
                    }
                }
            }
        }

        if (!alive)
        {
            KillCharacter();
        }
    }

    public void GetSpawnWeapon()
    {
        foreach(InventorySlot invSlot in inventory.inventorySlots)
        {
            if(invSlot.item != null)
            {
                Weapon weapon = invSlot.item.GetComponent<Weapon>();
                if (weapon != null)
                {
                    gun = weapon;
                    weapon.character = this;
                    invSlot.item.SetActive(true);
                    invSlot.item.transform.SetParent(this.transform);
                    invSlot.item.transform.position = this.transform.position;
                    invSlot.item.GetComponent<Collider2D>().enabled = false;
                    break;
                }
            }
        }
    }
}
