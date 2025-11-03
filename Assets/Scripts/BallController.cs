using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    
    public static float initialSpeed = 200f;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(0, initialSpeed, 0));
 
        
    }

    void Update()
    {
        if (rb.velocity.magnitude < 20)
        {
            rb.velocity = rb.velocity.normalized * 20;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Vector2 random2D = UnityEngine.Random.insideUnitCircle.normalized;
        rb.velocity += new Vector3(random2D.x, random2D.y, 0);
    }
}
