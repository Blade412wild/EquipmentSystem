using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Rock : MonoBehaviour, IGrabAble
{
    public Transform HoldPos { get; set; }
    public Rigidbody Rb { get; set; }
    private void Start()
    {
        HoldPos = GetComponentInChildren<Transform>();
        Rb = GetComponent<Rigidbody>();
    }

    public void HasBeenGrabed()
    {
        transform.localPosition = new Vector3 (0, 0, 0);
        OwnPhysics.FreezePosition(Rb);
    }


    public void HasBeenReleased()
    {
        OwnPhysics.RemoveConstraints(Rb);
    }


}
