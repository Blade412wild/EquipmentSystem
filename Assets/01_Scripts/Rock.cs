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
    }

    public void HasBeenGrabed()
    {
        Debug.Log(transform.name);
        transform.localPosition = new Vector3 (0, 0, 0);
    }


    public void HasBeenReleased()
    {


    }
}
