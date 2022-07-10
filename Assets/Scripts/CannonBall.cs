using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    Rigidbody rb;

    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            rb.isKinematic = true;
            //rb.drag = 20;
            //rb.velocity = Vector3.zero;
        }

        if(other.CompareTag("Target"))
        {
            Destroy(other.gameObject,0.1f);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            rb.isKinematic = true;
            //rb.drag = 20;
            //rb.velocity = Vector3.zero;
        }

        if (other.gameObject.CompareTag("Target"))
        {
            Destroy(other.gameObject, 0.1f);
        }
    }

}
