using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Hat : MonoBehaviour, IGrabAble
{
    public Transform HoldPos { get; set; }
    public Rigidbody Rb { get; set; }

    private void Start()
    {
        HoldPos = GetComponentInChildren<Transform>();
    }

    public void HasBeenGrabed()
    {
        Debug.Log(transform.name);
    }

    public void HasBeenReleased()
    {

    }
}
