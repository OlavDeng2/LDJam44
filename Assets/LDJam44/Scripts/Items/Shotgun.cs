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
            PooledObject bullet = bulletPool.GetObject();
            Bullet bulletScript = bullet.GetComponent<Bullet>();

            //set the angle of the bullet
            float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
            bullet.gameObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            bullet.transform.position = this.transform.position;
            bulletScript.shooter = character.gameObject;
            bulletScript.damage = bulletDamage;
            bulletScript.startPos = this.transform.position;
            bulletScript.maxRange = range;

            //Set random shoot direction based on cone based on bulletspread and aim direction
            bullet.GetComponent<Rigidbody2D>().velocity = shootDirection * speed;
            
        }

        if (shootAudioClips.Length > 0)
        {
            audioSource.PlayOneShot(shootAudioClips[Random.Range(0, shootAudioClips.Length)]);
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
