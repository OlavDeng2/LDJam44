using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Character
{
    [Header("Gun")]
    public ObjectPool bulletPool;
    public float bulletSpeed = 10;
    public int totalAmmo = 90;
    public int currentAmmoInMag = 30;
    public int maxAmmoInMag = 30;
    public float gunRange = 10;
    public float fireRate = 0.1f; //how many seconds between shots
    public float timeToReload = 2f;
    public LayerMask enemyLayer;

    [Header("Gun Data")]
    public float timeSinceLastShot = 0f;
    public float timeSinceReloadStart = 0f;
    public bool isReloading = false;

    [Header("UI")]
    public Text healthText;
    public Text ammoText;

    private void Start()
    {
        timeSinceLastShot = fireRate;
    }


    // Update is called once per frame
    void Update()
    {
        UpdateUI();

        //keep counter going to keep track of when shot was last fired
        timeSinceLastShot += Time.deltaTime;

        //Handle input
        MoveCharacter(Vector3.Normalize(new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0)));
        Vector3 lookDirection = Vector3.Normalize((Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10)) - this.transform.position);
        //LookDirection(lookDirection );


        if(!isReloading)
        {
            if (Input.GetButton("Fire1"))
            {
                ShootGun(lookDirection);
            }

            if (Input.GetButton("Reload"))
            {
                if(totalAmmo > 0)
                {
                    isReloading = true;
                }
            }
        }
        else if(isReloading)
        {
            timeSinceReloadStart += Time.deltaTime;
            if(timeSinceReloadStart > timeToReload)
            {
                ReloadGun();
                timeSinceReloadStart = 0f;
                isReloading = false;
            }
        }
    }

    private void ShootGun(Vector3 direction)
    {
        //Shoot if has more ammo than 0 in mag
        if(currentAmmoInMag > 0)
        {
            if (timeSinceLastShot >= fireRate)
            {
                timeSinceLastShot = 0f;
                currentAmmoInMag -= 1;

                //New shoot
                PooledObject bullet = bulletPool.GetObject();
                bullet.transform.position = this.transform.position;
                bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
                bullet.GetComponent<Bullet>().shooter = this.gameObject;
                bullet.GetComponent<Bullet>().damage = damage;


                /*
                //Old shoot
                //shoot
                RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, gunRange, enemyLayer);

                Debug.DrawRay(transform.position, direction * gunRange, Color.yellow, 5f);

                // Does the ray intersect any objects excluding the player layer
                if (hit)
                {
                    hit.collider.gameObject.GetComponent<Character>().TakeDamage(damage);
                }*/
            }
        }
    }

    private void ReloadGun()
    {
        if(totalAmmo > maxAmmoInMag)
        {
            int ammoToAdd = maxAmmoInMag - currentAmmoInMag;
            totalAmmo -= ammoToAdd;

            currentAmmoInMag = maxAmmoInMag;
        }

        //If total ammo is more than 0, but less than 30(checked by previous if statement)
        else if (totalAmmo > 0)
        {
            int ammoToAdd = maxAmmoInMag - currentAmmoInMag;

            //if the total ammo is more than requested, just fill the mag as usual
            if(totalAmmo >= ammoToAdd)
            {
                currentAmmoInMag = maxAmmoInMag;
                totalAmmo -= ammoToAdd;
            }

            //If total ammo is smaller than ammo to add, add whatever ammo is rest to the magazine
            else if(totalAmmo <= ammoToAdd)
            {
                currentAmmoInMag += totalAmmo;
                totalAmmo = 0;
            }
        }
    }

    private void UpdateUI()
    {
        ammoText.text = currentAmmoInMag + "/" + totalAmmo;
        healthText.text = health.ToString();
    }
}
