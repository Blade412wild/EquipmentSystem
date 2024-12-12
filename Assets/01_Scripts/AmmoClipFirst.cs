using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AmmoClipFirst : MonoBehaviour, IGrabAble, IPlaceAble, IActivateable
{
    static public event Action OnAmmoActivation;
    public Transform HoldPos { get; set; }
    public Rigidbody Rb { get; set; }
    public bool InArea { get; set; }
    public Transform PlacementParentTrans { get; set; }
    public Interactor Interactor { get; set; }
    private Collider collider;




    private void Start()
    {
        SetVariables();
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

    public void Activate()
    {
        Destroy(this.gameObject);
    }

    public void OnPrimaryButton()
    {
    }
}
