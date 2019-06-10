using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon
{
    [Header("Settings")]
    public int shotgunBulletCount = 5;
    public float bulletSPread = 1;

    public override void FireGun()
    {
        //TODO: Figure out stuff for multiple bullets

        Vector3 aimDirection = Vector3.Normalize((Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10)) - this.transform.position);

        //New shoot
        PooledObject bullet = player.bulletPool.GetObject();
        bullet.transform.position = this.transform.position;
        bullet.GetComponent<Rigidbody2D>().velocity = aimDirection * speed;
        bullet.GetComponent<Bullet>().shooter = player.gameObject;
        bullet.GetComponent<Bullet>().damage = bulletDamage;

        canUseItem = false;
    }
}
