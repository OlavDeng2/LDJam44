using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    public enum FireMode { Manual, SemiAuto, FullAuto}

    [Header("References")]
    public InventoryItem ammo;

    [Header("Settings")]
    public List<FireMode> availableFireModes;
    public int ammoCapacity = 30;
    public int currentAmmo = 0;
    public float reloadTime = 5;
    public float manualCyclingTime = 2;
    public float semiCyclingTime = 1;
    public float automaticCyclingTime = 1;
    public int range = 10;
    public float speed = 10;
    public float bulletDamage = 10;

    [Header("Data")]
    public int currentFireMode = 0;
    public float currentCycleTime = 0;
    public bool isCyclingGun = false;
    public bool isReloading = false;

    public override void Update()
    {
        //cycle the gun
        if(isCyclingGun)
        {
            currentTime += Time.deltaTime;
            if (currentCycleTime <= currentTime)
            {
                //if weapon is
                if((availableFireModes[currentFireMode] != FireMode.SemiAuto))
                {
                    canUseItem = true;

                }
                currentTime = 0;
                CycleBolt();
                isCyclingGun = false;
            }
        }

        if(isReloading)
        {
            canUseItem = false;
            currentTime += Time.deltaTime;
            if (reloadTime <= currentTime)
            {
                canUseItem = true;
                currentTime = 0;
                Reload();
                isReloading = false;
            }
        }
    }

    public override void UseItem()
    {
        if (canUseItem && currentAmmo > 0)
        {

            currentAmmo -= 1;

            if (availableFireModes[currentFireMode] == FireMode.Manual)
            {

                Debug.Log("manual fire");

                FireGun();

                currentCycleTime = manualCyclingTime;
            }

            if (availableFireModes[currentFireMode] == FireMode.SemiAuto)
            {

                Debug.Log("semi auto fire");

                FireGun();

                currentCycleTime = semiCyclingTime;
            }

            if (availableFireModes[currentFireMode] == FireMode.FullAuto)
            {

                Debug.Log("Full auto fire");
                FireGun();


                currentCycleTime = automaticCyclingTime;
                isCyclingGun = true;
            }
        }
    }

    public virtual void FireGun()
    {
        Vector3 aimDirection = Vector3.Normalize((Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10)) - this.transform.position);

        //New shoot
        PooledObject bullet = player.bulletPool.GetObject();
        bullet.transform.position = this.transform.position;
        bullet.GetComponent<Rigidbody2D>().velocity = aimDirection * speed;
        bullet.GetComponent<Bullet>().shooter = player.gameObject;
        bullet.GetComponent<Bullet>().damage = bulletDamage;

        canUseItem = false;

    }

    public void ChangeFireMode()
    {

        Debug.Log("switching fire mode");
        currentFireMode += 1;
        if(currentFireMode >= availableFireModes.Count)
        {
            currentFireMode = 0;
        }
    }

    public void CycleBolt()
    {
        isCyclingGun = true;
    }

    public void Reload()
    {
        List<InventorySlot> inventorySlots = new List<InventorySlot>();
        int totalAmmo = 0;
        int ammoToRemoveFromInv = 0;

        Debug.Log(totalAmmo);

        //Find the slots of the appropriate ammo and 
        foreach (InventorySlot invSlot in player.inventory.inventorySlots)
        {
            if(invSlot.item == ammo)
            {
                inventorySlots.Add(invSlot);
                totalAmmo += invSlot.amount;
                Debug.Log(totalAmmo);
            }
        }

        
        if (totalAmmo > ammoCapacity)
        {
            int ammoToAdd = ammoCapacity - currentAmmo;
            totalAmmo -= ammoToAdd;
            ammoToRemoveFromInv = ammoToAdd;

            currentAmmo = ammoCapacity;
        }

        //If total ammo is more than 0, but less than 30(checked by previous if statement)
        else if (totalAmmo > 0)
        {
            int ammoToAdd = ammoCapacity - currentAmmo;

            //if the total ammo is more than requested, just fill the mag as usual
            if (totalAmmo >= ammoToAdd)
            {
                currentAmmo = ammoCapacity;
                totalAmmo -= ammoToAdd;

                ammoToRemoveFromInv = ammoToAdd;
            }

            //If total ammo is smaller than ammo to add, add whatever ammo is rest to the magazine
            else if (totalAmmo <= ammoToAdd)
            {
                currentAmmo += totalAmmo;
                totalAmmo = 0;
                ammoToRemoveFromInv = totalAmmo;

            }
        }

        //Remove ammo from the inventory
        foreach(InventorySlot invSlot in inventorySlots)
        {

            //if there is more ammo than the amount to remove, only remove the amount to remove from slot and break from loop
            if (invSlot.amount > ammoToRemoveFromInv)
            {
                invSlot.amount -= ammoToRemoveFromInv;
                break;
            }

            //if there is less ammo than amount to remove, remove it all and loop back
            else if (invSlot.amount <= ammoToRemoveFromInv)
            {
                ammoToRemoveFromInv -= invSlot.amount;
                invSlot.RemoveItem();
            }
        }
    }
}
