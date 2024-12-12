using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody rb;
    private CapsuleCollider collider;



    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<CapsuleCollider>();
    }

    public void ActivateBullet(float force)
    {
        rb.AddForce(Vector3.forward * force, ForceMode.Impulse);
        collider.enabled = true;
    }
}
