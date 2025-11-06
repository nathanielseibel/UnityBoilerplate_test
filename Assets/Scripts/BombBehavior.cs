using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBehavior : MonoBehaviour
{
    // Speed at which the bomb falls
    public float fallSpeed = 5f;
    public float bounceForce = 300f; // Adjust how high bomb bounces

    //Base Prefab reference
    [SerializeField] private GameObject basePrefab;

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

    void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        // Check if bomb hit the paddle
        if (!hasHitPaddle && collision.gameObject.CompareTag("Player"))
        {
            hasHitPaddle = true;

            // Shoot bomb back into the air
            if (rb != null)
            {
                rb.velocity = new Vector3(rb.velocity.x, bounceForce *2f, rb.velocity.z);
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

        // Add a small random vector to the bomb's velocity to prevent straight lines
        Vector2 random2D = UnityEngine.Random.insideUnitCircle.normalized;
        rb.velocity += new Vector3(random2D.x, random2D.y, 0);
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
