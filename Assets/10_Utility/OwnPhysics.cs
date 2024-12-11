using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class OwnPhysics
{
    static public void FreezePosition(Rigidbody rb)
    {
        rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
    }

    static public void UnFreezePosition(Rigidbody rb)
    {
        rb.constraints = RigidbodyConstraints.None;
    }

    static public void ResetVelocity(Rigidbody rb)
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}
