using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    public enum FireMode { Manual, SemiAuto, FullAuto}

    [Header("References")]
    public Item ammo;
    public ObjectPool bulletPool;

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
    public Vector3 aimDirection = new Vector3(0, 0, 0);

    public event EventHandler<WeaponEventsArgs> startReload;
    public event EventHandler<WeaponEventsArgs> endReload;
    public event EventHandler<WeaponEventsArgs> fireGun;

    [Header("Audio")]
    public AudioClip[] shootAudioClips;
    public AudioClip[] cycleBoltAudioClips;
    public AudioClip[] changeFireModeAudioClips;
    public AudioClip[] reloadAudioClips;

    public override void Update()
    {
        if(isReloading)
        {
            canUseItem = false;
            currentTime += Time.deltaTime;
            if (reloadTime <= currentTime)
            {
                canUseItem = true;
                currentTime = 0;
                FinishReload();
            }
        }

        else if(!isReloading)
        {
            //cycle the gun
            if (isCyclingGun)
            {
                currentTime += Time.deltaTime;
                if (currentCycleTime <= currentTime)
                {
                    //if weapon is
                    if ((availableFireModes[currentFireMode] != FireMode.SemiAuto))
                    {
                        canUseItem = true;
                    }
                    currentTime = 0;
                    FinishCycleBolt();
                }
            }
        }
    }

    public override void UseItem()
    {
        if (canUseItem && currentAmmo > 0)
        {
            currentAmmo -= 1;

            FireGunEvent(this);

            if (availableFireModes[currentFireMode] == FireMode.Manual)
            {
                FireGun();

                currentCycleTime = manualCyclingTime;
            }

            if (availableFireModes[currentFireMode] == FireMode.SemiAuto)
            {
                FireGun();

                currentCycleTime = semiCyclingTime;
            }

            if (availableFireModes[currentFireMode] == FireMode.FullAuto)
            {
                FireGun();

                currentCycleTime = automaticCyclingTime;
                StartCycleBolt();
            }
        }
    }

    public virtual void FireGun()
    {
        //New shoot
        PooledObject bullet = bulletPool.GetObject();
        Bullet bulletScript = bullet.GetComponent<Bullet>();

        bullet.transform.position = this.transform.position;

        //set the angle of the bullet
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        bullet.gameObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);


        bullet.GetComponent<Rigidbody2D>().velocity = aimDirection * speed;
        bulletScript.shooter = character.gameObject;
        bulletScript.damage = bulletDamage;
        bulletScript.startPos = this.transform.position;
        bulletScript.maxRange = range;


        if (shootAudioClips.Length > 0)
        {
            audioSource.PlayOneShot(shootAudioClips[UnityEngine.Random.Range(0, shootAudioClips.Length)]);
        }

        canUseItem = false;
    }

    public void ChangeFireMode()
    {

        currentFireMode += 1;
        if(currentFireMode >= availableFireModes.Count)
        {
            currentFireMode = 0;
        }

        if (changeFireModeAudioClips.Length > 0)
        {
            audioSource.PlayOneShot(changeFireModeAudioClips[UnityEngine.Random.Range(0, changeFireModeAudioClips.Length)]);
        }
    }

    public void StartCycleBolt()
    {
        isCyclingGun = true;

        if (cycleBoltAudioClips.Length > 0)
        {
            audioSource.PlayOneShot(cycleBoltAudioClips[UnityEngine.Random.Range(0, cycleBoltAudioClips.Length)]);
        }
    }

    private void FinishCycleBolt()
    {
        isCyclingGun = false;

    }

    public void StartReload()
    {
        isReloading = true;

        StartReloadEvent(this);

        if (reloadAudioClips.Length > 0)
        {
            audioSource.PlayOneShot(reloadAudioClips[UnityEngine.Random.Range(0, reloadAudioClips.Length)]);
        }
    }

    private void FinishReload()
    {
        isReloading = false;

        List<InventorySlot> inventorySlots = new List<InventorySlot>();
        int totalAmmo = 0;
        int ammoToRemoveFromInv = 0;

        //Find the slots of the appropriate ammo and 
        foreach (InventorySlot invSlot in character.inventory.inventorySlots)
        {
            if (invSlot.item)
            {
                if(invSlot.item.GetComponent<Item>() is Ammo)
                {
                    inventorySlots.Add(invSlot);
                    totalAmmo += invSlot.amount;
                }
                
            }
        }


        if (totalAmmo > ammoCapacity)
        {
            int ammoToAdd = ammoCapacity - currentAmmo;
            totalAmmo -= ammoToAdd;
            ammoToRemoveFromInv = ammoToAdd;

            currentAmmo = ammoCapacity;
        }

        //If total ammo is more than 0, but less than totalammo(checked by previous if statement)
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
                ammoToRemoveFromInv = totalAmmo;
                totalAmmo = 0;

            }
        }

        //Remove ammo from the inventory
        foreach (InventorySlot invSlot in inventorySlots)
        {

            //if there is more ammo than the amount to remove, only remove the amount to remove from slot and break from loop
            if (invSlot.amount > ammoToRemoveFromInv)
            {
                invSlot.amount -= ammoToRemoveFromInv;
                invSlot.item.GetComponent<Item>().amount -= ammoToRemoveFromInv;
                if(invSlot.amountText != null)
                {
                    invSlot.amountText.text = invSlot.amount.ToString();
                }
                break;
            }

            //if there is less ammo than amount to remove, remove it all and loop back
            else if (invSlot.amount <= ammoToRemoveFromInv)
            {
                ammoToRemoveFromInv -= invSlot.amount;
                invSlot.RemoveItem();
            }
        }
        EndReloadEvent(this);
    }


    public void StartReloadEvent(Weapon weapon)
    {
        if (startReload != null)
        {
            startReload(this, new WeaponEventsArgs(weapon));
        }
    }

    public void EndReloadEvent(Weapon weapon)
    {
        if (endReload != null)
        {
            endReload(this, new WeaponEventsArgs(weapon));
        }
    }

    public void FireGunEvent(Weapon weapon)
    {
        if (startReload != null)
        {
            fireGun(this, new WeaponEventsArgs(weapon));
        }
    }
}


public class WeaponEventsArgs : EventArgs
{
    public WeaponEventsArgs(Weapon weapon)
    {
        Weapon = weapon;
    }

    public Weapon Weapon;
}