using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBehavior : MonoBehaviour
{
    // Speed at which the bomb falls
    public float fallSpeed = 5f;
    public float bounceForce = 30f; // Adjust how high bomb bounces
    private float bombHP = 1f;

    //Explosion Prefab reference
    [SerializeField] private GameObject explosion;

    private bool hasHitPaddle = false;
    private Collider bombCollider;

    private void Awake()
    {
        bombCollider = GetComponent<Collider>();

        //The bomb flys upward when instantiated
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 launchDirection = (Vector3.up).normalized;
            rb.AddForce(launchDirection * 15f, ForceMode.Impulse);
        }

        //Set this bomb to ignore collisions with Brick, SpeedBrick, TankyBrick, SuperTankyBrick
        Collider[] allColliders = FindObjectsOfType<Collider>();
        foreach (Collider col in allColliders)
        {
            if (col.CompareTag("Brick") || col.CompareTag("SpeedBrick") || col.CompareTag("TankyBrick") || col.CompareTag("SuperTankyBrick"))
            {
                Physics.IgnoreCollision(bombCollider, col);
            }
        }

        //Code to ignore collisions with all balls in the scene
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        foreach (GameObject ball in balls)
        {
            Collider ballCollider = ball.GetComponent<Collider>();
            if (ballCollider != null)
            {
                Physics.IgnoreCollision(bombCollider, ballCollider, true);
            }
        }
    }

    private void Update()
    {
        
    }

    public void TakeDamage()
    {
        bombHP--;

        if (bombHP <= 0)
        {
            // 1. Instantiate the explosion FIRST.
            Instantiate(explosion, transform.position, Quaternion.identity);

            // 2. Then destroy the current object.
            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        // Check if bomb hit the paddle
        if (!hasHitPaddle && collision.gameObject.CompareTag("Player"))
        {
            hasHitPaddle = true;

            // Shoot bomb back into the air everytime it hits the paddle
            if (rb != null)
            {
                rb.velocity = new Vector3(rb.velocity.x, bounceForce, rb.velocity.z);
            }



            //if player is moving, add some of that velocity to the bomb
            Rigidbody paddleRb = collision.gameObject.GetComponent<Rigidbody>();
            if (paddleRb != null)
            {
                rb.velocity += new Vector3(paddleRb.velocity.x * 0.5f, 0, 0);
            }


            // Re-enable collisions with bricks
            EnableBrickCollisions();
        }

        //if it hits the "Wall" bounce the ball into the air
        if (collision.gameObject.CompareTag("Wall"))
        {
            if (rb != null)
            {
                rb.velocity = new Vector3(rb.velocity.x, bounceForce, rb.velocity.z);
            }
        }

        
    }

    


    void EnableBrickCollisions()
    {
        Collider[] allColliders = FindObjectsOfType<Collider>();
        foreach (Collider col in allColliders)
        {
            if (col.CompareTag("Brick") || col.CompareTag("SpeedBrick") || col.CompareTag("TankyBrick") || col.CompareTag("SuperTankyBrick"))
            {
                Physics.IgnoreCollision(bombCollider, col, false);
            }
        }
    }

   
}
