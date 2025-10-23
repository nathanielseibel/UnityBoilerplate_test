using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float ballSpeed = 15f; // Adjust the ball speed as needed
    private Rigidbody rb;
    


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Set initial ball movement direction
        rb.velocity = Vector2.up * ballSpeed;
    }

    void OnCollisionEnter3D(Collision collision)
    {
        // Check if the ball collides with walls, paddles, or bricks
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Brick"))
        {
            // Reflect the ball's velocity upon collision
            Vector2 reflection = Vector2.Reflect(rb.velocity, collision.contacts[0].normal);
            rb.velocity = reflection.normalized * ballSpeed;
        }
    }
}
