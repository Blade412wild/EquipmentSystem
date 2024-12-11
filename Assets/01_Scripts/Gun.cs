using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Gun : MonoBehaviour, IGrabAble, IActivateable
{
    public Transform HoldPos { get; set; }
    public Rigidbody Rb { get; set; }

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
    }

    public void Activate()
    {
        Schoot();
    }

    private void Schoot()
    {
        Debug.Log("bang");
    }
}
