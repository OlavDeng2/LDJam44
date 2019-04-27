using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public int totalAmmo = 90;
    public int currentAmmoInMag = 30;
    public int maxAmmoInMag = 30;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ShootGun()
    {
        //Shoot if has more ammo than 0 in mag
        if(currentAmmoInMag > 0)
        {
            //shoot
        }
    }

    private void ReloadGun()
    {
        int ammoToAdd = currentAmmoInMag - maxAmmoInMag;
        totalAmmo -= ammoToAdd;

        currentAmmoInMag = maxAmmoInMag;
    }
}
