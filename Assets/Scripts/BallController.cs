using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [Header("Launch Settings")]
    [SerializeField] private float launchSpeed = 10f;
    [SerializeField] private Vector2 launchDirection = new Vector2(0.5f, 1f); // Angle of launch

    [Header("Paddle Reference")]
    [SerializeField] private Transform paddle;
    [SerializeField] private Vector3 offsetFromPaddle = new Vector3(0f, 0.5f, 0f); // Position above paddle

    private bool isLaunched = false;
    public static float initialSpeed = 300f;
    Rigidbody rb;

    //speed for whatever direction the ball is moving
    [SerializeField] private float ballSpeed = 35f;
    //variable for speedup timer
    [SerializeField] private float speedUpDuration = 2f;



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(0, initialSpeed, 0));
        // Make sure ball doesn't move until launched
        rb.isKinematic = true;

        
    }

    // Launch the ball
    private void LaunchBall()
    {
        isLaunched = true;

        // Enable physics
        rb.isKinematic = false;

        // Launch the ball
        rb.velocity = launchDirection.normalized * launchSpeed;
    }
    // Optional: Reset function for when player loses a life
    public void ResetBall()
    {
        isLaunched = false;
        rb.isKinematic = true;
        rb.velocity = Vector2.zero;
        transform.position = paddle.position + offsetFromPaddle;
    }

    void Update()
    {

        // If not launched, stick ball to paddle
        if (!isLaunched)
        {
            // Keep ball stuck to paddle
            transform.position = paddle.position + offsetFromPaddle;

            // Check for spacebar press
            if (Input.GetKeyDown(KeyCode.Space))
            {
                LaunchBall();
            }
        }

        //apply a downward force to the ball constantly
        rb.AddForce(new Vector3(0, -2f, 0));

        //if the ball is moving use the ball speed variable to set the velocity
        if (rb.velocity.magnitude > 0)
        {
            rb.velocity = rb.velocity.normalized * ballSpeed;
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        // Add a small random vector to the ball's velocity to prevent straight lines
        Vector2 random2D = UnityEngine.Random.insideUnitCircle.normalized;
        rb.velocity += new Vector3(random2D.x, random2D.y, 0);

        //If the ball hits the paddle, increase speed temporarily by double
        if (collision.gameObject.CompareTag("Player"))
        {
            ballSpeed = 50f;
            // after 2 seconds, reset speed back to normal
            StartCoroutine(ResetBallSpeed());
            //reset the duration each hit
            speedUpDuration = 2f;
            Debug.Log("Ball hit, speed up duration reset");
        }


    }
    private IEnumerator ResetBallSpeed()
    {
        yield return new WaitForSeconds(speedUpDuration);
        ballSpeed = 35f;
    }
}
