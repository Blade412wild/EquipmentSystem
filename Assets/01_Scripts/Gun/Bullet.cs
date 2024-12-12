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

    public void ActivateBullet(float force, Transform spawnpoint)
    {
        OwnPhysics.FreezeRotation(rb);
        rb.AddForce(spawnpoint.forward * force, ForceMode.Impulse);
        
        collider.enabled = true;
        transform.parent = null;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }
}
