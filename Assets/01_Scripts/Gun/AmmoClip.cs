using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AmmoClip : MonoBehaviour
{
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private List<Transform> bulletSpots = new List<Transform>();
    [SerializeField] private List<Bullet> bullets;


    private void Start()
    {
        CreateBullets();
    }

    public Bullet TakeBullet()
    {
        if (bullets.Count <= 0) return null;

        Bullet bullet;
        bullet = bullets[0];
        bullets.Remove(bullet);
        SortBullets();
        return bullet;
    }
    public void Reload()
    {
        EmptyClip();
        CreateBullets();
    }

    private void CreateBullets()
    {
        bullets = new List<Bullet>();

        for (int i = 0; i < bulletSpots.Count; i++)
        {
            Bullet bullet = Instantiate(bulletPrefab, bulletSpots[i]);
            bullets.Add(bullet);
        }
    }

    private void SortBullets()
    {
        for (int i = 0; i < bullets.Count; i++)
        {
            bullets[i].transform.SetParent(bulletSpots[i].transform);
            bullets[i].transform.localPosition = new Vector3(0, 0, 0);
        }
    }

    private void EmptyClip()
    {
        if (bullets.Count <= 0) return;

        List<Bullet> removebullets = new List<Bullet>();

        for (int i = 0; i < bullets.Count; i++)
        {
            Destroy(bullets[i].gameObject);
        }
    }
}
