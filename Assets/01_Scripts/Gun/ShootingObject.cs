using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class ShootingObject : MonoBehaviour
{
    public float timeBetweenShot = 1;
    public Transform bulletSpawnTrans;
    public GameObject Bullet;
    private Timer timer;
    private bool MayShoot = true;
    public AmmoClip ammoClip;

    public virtual void Shoot(float force)
    {
        if (MayShoot != true) return;


        Bullet bullet = ammoClip.TakeBullet();
        if (bullet != null)
        {
            bullet.transform.position = bulletSpawnTrans.position;
            bullet.ActivateBullet(force);
            ResetShot();
        }
        else
        {
            // play empty sound clip
            MayShoot = false;
        }

    }

    public virtual void Reload()
    {

    }

    public virtual void ResetShot()
    {
        MayShoot = true;
    }

    public virtual void CreateBullet()
    {

    }
}
