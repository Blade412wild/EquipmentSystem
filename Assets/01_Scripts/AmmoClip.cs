using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AmmoClip : MonoBehaviour, IGrabAble, IPlaceAble
{
    public Transform HoldPos { get; set; }
    public Rigidbody Rb { get; set; }
    public bool InArea { get; set; }
    public Transform PlacementParentTrans { get; set; }

    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private List<Transform> bulletSpots = new List<Transform>();

    [SerializeField] private List<Bullet> bullets;
    private int bulletsCount;
    private Collider collider;




    private void Start()
    {
        SetVariables();
        CreateBullets();
    }

    private void Update()
    {

    }

    public void HasBeenGrabed()
    {
        Vector3 MovePostion = new Vector3(-HoldPos.localPosition.x, -HoldPos.localPosition.y, -HoldPos.localPosition.z);
        transform.localPosition = MovePostion;
        OwnPhysics.FreezePositionAndRotation(Rb);
    }


    public void HasBeenReleased()
    {
        OwnPhysics.RemoveConstraints(Rb);
    }

    public void SetVariables()
    {
        HoldPos = GetComponentInChildren<HoldPos>().transform;
        Rb = GetComponent<Rigidbody>();
        collider = GetComponent<BoxCollider>();
    }

    public void PlaceItem()
    {
        if (InArea)
        {
            collider.isTrigger = true;
            transform.SetParent(PlacementParentTrans);
            transform.position = PlacementParentTrans.position;
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        else
        {
            HasBeenReleased();
        }
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
}
