using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float ballSpeed = 5f; // Adjust the ball speed as needed
    private Rigidbody rb;
    


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Vector2 force = Vector2.zero;
        force.x = Random.Range(-1f, 1f);
        force.y = -1f;
        
        rb.velocity = Vector2.up * ballSpeed;
    }
  
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Brick"))
        {
            
            Vector2 reflection = Vector2.Reflect(rb.velocity, collision.contacts[0].normal);
            rb.velocity = reflection.normalized * ballSpeed;
        } else if (collision.gameObject.CompareTag("Player"))
        {
            
            Vector2 playerCenter = collision.collider.transform.position;
            
            Vector2 point = collision.contacts[0].point;

            rb.velocity = (point - playerCenter).normalized * rb.velocity.magnitude;

            if (rb.velocity.magnitude < ballSpeed)
                rb.velocity = rb.velocity.normalized * ballSpeed;
        }
    }
}
