using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Pistol : ShootingObject, IGrabAble, IActivateable
{
    private enum PistolMode { Single, Automatic }
    private PistolMode mode = PistolMode.Single;
    public Transform HoldPos { get; set; }
    public Rigidbody Rb { get; set; }
    public Interactor Interactor { get; set; }

    private bool TriggerIsPressed = false;


    private StateMachine stateMachine;

    private void Start()
    {
        SetVariables();
    }

    private void Update()
    {
        if (mode == PistolMode.Automatic && TriggerIsPressed)
        {
            base.Shoot(15);
        }
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
        Rb.velocity = Interactor.Rb.velocity;
    }

    public void SetVariables()
    {
        HoldPos = GetComponentInChildren<HoldPos>().transform;
        Rb = GetComponent<Rigidbody>();
    }

    public void Activate()
    {
        TriggerIsPressed = true;
        if (mode == PistolMode.Single)
        {
            base.Shoot(20);
        }

    }

    public void OnSecondaryButton()
    {
        if(mode == PistolMode.Automatic)
        {
            mode = PistolMode.Single;
        }
        else
        {
            mode = PistolMode.Automatic;
        }
    }

    public void DeActivate()
    {
        TriggerIsPressed = false;
    }
}
