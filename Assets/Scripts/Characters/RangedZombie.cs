using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedZombie : Character
{
    [Header("Gun")]
    public ObjectPool bulletPool;
    public float bulletSpeed = 10;
    public int totalAmmo = 90;
    public int currentAmmoInMag = 30;
    public int maxAmmoInMag = 30;
    public float fireRange = 8;
    public float gunRange = 10;
    public float fireRate = 0.1f; //how many seconds between shots
    public float timeToReload = 2f;
    public LayerMask enemyLayer;

    [Header("Gun Data")]
    public float timeSinceLastShot = 0f;
    public float timeSinceReloadStart = 0f;
    public bool isReloading = false;

    public GameObject player;
    public Player playerScript;
    // Start is called before the first frame update
    void Start()
    {
        //This just assumes that there will be only one player
        playerScript = FindObjectOfType<Player>();
        player = playerScript.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(GetPlayerDistance() >= fireRange)
        {
            MoveCharacter(GetPlayerDirection());
        }
        LookDirection(GetPlayerDirection());

        if (!alive)
        {
            this.GetComponent<PooledObject>().ReturnToPool();
        }


        //Shoot the gun
        timeSinceLastShot += Time.deltaTime;

        if (!isReloading)
        {
            if (GetPlayerDistance() <= fireRange)
            {
                if(currentAmmoInMag > 0)
                {
                    ShootGun(GetPlayerDirection());
                }

                else if (currentAmmoInMag <= 0)
                {
                    if (totalAmmo > 0)
                    {
                        isReloading = true;
                    }
                }
            }
        }

        else if (isReloading)
        {
            timeSinceReloadStart += Time.deltaTime;
            if (timeSinceReloadStart > timeToReload)
            {
                ReloadGun();
                timeSinceReloadStart = 0f;
                isReloading = false;
            }

        }
    }

    private Vector3 GetPlayerDirection()
    {
        if (player)
        {
            Vector3 directionToPlayer = Vector3.Normalize(player.transform.position - this.gameObject.transform.position);
            return directionToPlayer;
        }

        else
        {
            return new Vector3(0, 0, 0);
        }

    }
    private float GetPlayerDistance()
    {
        if (player)
        {
            float distanceToPlayer = Vector3.Magnitude(player.transform.position - this.gameObject.transform.position);
            return distanceToPlayer;
        }

        else
        {
            return 0;
        }

    }


    private void ShootGun(Vector3 direction)
    {
        //Shoot if has more ammo than 0 in mag
        if (currentAmmoInMag > 0)
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

            }
        }
    }

    private void ReloadGun()
    {
        if (totalAmmo > maxAmmoInMag)
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
            if (totalAmmo >= ammoToAdd)
            {
                currentAmmoInMag = maxAmmoInMag;
                totalAmmo -= ammoToAdd;
            }

            //If total ammo is smaller than ammo to add, add whatever ammo is rest to the magazine
            else if (totalAmmo <= ammoToAdd)
            {
                currentAmmoInMag += totalAmmo;
                totalAmmo = 0;
            }

        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
