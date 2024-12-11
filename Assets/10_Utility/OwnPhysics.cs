using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class OwnPhysics
{
    static public void FreezePosition(Rigidbody rb)
    {
        rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
    }

    static public void FreezePositionAndRotation(Rigidbody rb)
    {
        rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ
                        |RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
    }

    static public void RemoveConstraints(Rigidbody rb)
    {
        rb.constraints = RigidbodyConstraints.None;
    }


    static public void ResetVelocity(Rigidbody rb)
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}
