using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody rb;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void ActivateBullet(float force)
    {
        rb.AddForce(Vector3.forward * force, ForceMode.Impulse);
    }
}
