using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class ShootingObject : MonoBehaviour
{
    public float timeBetweenShot = 0.2f;
    public Transform bulletSpawnTrans;
    public GameObject Bullet;
    public AmmoClip ammoClip;
    private Timer timer;
    private bool MayShoot = true;
    private bool readyToShoot = true;


    public virtual void Shoot(float force)
    {
        if (readyToShoot != true) return;


        Bullet bullet = ammoClip.TakeBullet();
        if (bullet != null)
        {
            readyToShoot = false;


            bullet.transform.position = bulletSpawnTrans.position;
            bullet.ActivateBullet(force, bulletSpawnTrans);

            timer = new Timer(timeBetweenShot);
            timer.OnTimerIsDone += ResetShot;
            //Invoke("ResetShot", timeBetweenShot);
        }
        else
        {
            // play empty sound clip
        }

    }

    public virtual void Reload()
    {
        ammoClip.Reload();
        ResetShot();
    }

    public virtual void ResetShot()
    {
        timer.OnTimerIsDone -= ResetShot;
        readyToShoot = true;
    }
}
