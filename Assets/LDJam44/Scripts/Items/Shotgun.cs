using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon
{
    [Header("Settings")]
    public int shotgunBulletCount = 5;
    public float bulletSpread = 1; //in degrees

    public override void FireGun()
    {

        Vector3 aimDirection = Vector3.Normalize((Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10)) - this.transform.position);

        //spawn bullets based on amount of bullets
        for(int i = 0; i <= shotgunBulletCount; i++)
        {
            float randomAngle = Random.Range(-bulletSpread, bulletSpread);

            Vector3 shootDirection = RotateVector(aimDirection, randomAngle);
            //Get bullet
            PooledObject bullet = player.bulletPool.GetObject();
            bullet.transform.position = this.transform.position;
            bullet.GetComponent<Bullet>().shooter = player.gameObject;
            bullet.GetComponent<Bullet>().damage = bulletDamage;

            //Set random shoot direction based on cone based on bulletspread and aim direction
            bullet.GetComponent<Rigidbody2D>().velocity = shootDirection * speed;
            
        }

        if (shootAudioClip != null)
        {
            audioSource.PlayOneShot(shootAudioClip);
        }

        canUseItem = false;
    }

    public Vector2 RotateVector(Vector2 v, float angle)
    {
        float radian = angle * Mathf.Deg2Rad;
        float _x = v.x * Mathf.Cos(radian) - v.y * Mathf.Sin(radian);
        float _y = v.x * Mathf.Sin(radian) + v.y * Mathf.Cos(radian);
        return new Vector2(_x, _y);
    }
}
