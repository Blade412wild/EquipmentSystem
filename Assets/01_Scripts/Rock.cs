using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Rock : MonoBehaviour, IGrabAble
{
    public Transform HoldPos { get; set; }
    public Rigidbody Rb { get; set; }
    public Interactor Interactor { get; set; }

    private void Start()
    {
        SetVariables();
    }

    public void HasBeenGrabed(Interactor interactor)
    {
        Interactor = interactor;
        Vector3 MovePostion = new Vector3(-HoldPos.localPosition.x, -HoldPos.localPosition.y, -HoldPos.localPosition.z);
        transform.localPosition = MovePostion;
        transform.rotation = new Quaternion(0, 0, 0, 0);
        OwnPhysics.FreezePositionAndRotation(Rb);
    }


    public void HasBeenReleased()
    {
        OwnPhysics.RemoveConstraints(Rb);
        Rb.velocity = Interactor.Rb.velocity *2;
    }

    public void SetVariables()
    {
        HoldPos = GetComponentInChildren<Transform>();
        Rb = GetComponent<Rigidbody>();
    }
}
