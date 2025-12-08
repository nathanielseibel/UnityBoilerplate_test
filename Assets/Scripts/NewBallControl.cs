using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class NewBallControl : MonoBehaviour
{
    [SerializeField] private AudioClip throwSound;
    [SerializeField] private AudioClip chargeSound;

    private AudioSource audioSource;

    //ParticleEffect Object
    [SerializeField] private GameObject chargedEffect;

    [Header("Launch Settings")]
    [SerializeField] private float launchSpeed = 0f;
    

    [Header("Paddle Reference")]
    [SerializeField] private Transform paddle;
    [SerializeField] private Vector3 offsetFromPaddle = new Vector3(0f, 0.5f, 0f); // Position above paddle

    private bool isLaunched = false;
    public static float initialSpeed = 300f;
    Rigidbody rb;

    //speed for whatever direction the ball is moving
    public float ballSpeed = 0f;
    //variable for speedup timer
    [SerializeField] private float speedUpDuration = 2f;

    [SerializeField] private float maxCharge = 100f;
    [SerializeField] private float chargeRate = 50f;
    private float currentCharge = 0f;

    [SerializeField] private float minBallSpeed = 25f;  // Minimum speed after decay
    [SerializeField] private float decayDuration = 5f;  // Time to reach min speed (seconds)

    private float launchTime;  // Track when ball was launched


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(0, initialSpeed, 0));
        // Make sure ball doesn't move until launched
        rb.isKinematic = true;
    }
    // Launch the ball
    private void LaunchBall()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Mathf.Abs(Camera.main.transform.position.z);
        Vector3 mouseGamePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        
        rb.isKinematic = false;

        Vector3 launchDirection = (mouseGamePosition - rb.position).normalized;
        // Launch the ball
        if (rb != null)
        {
            
            ballSpeed = launchSpeed;
            launchTime = Time.time;
            rb.velocity = launchDirection * launchSpeed;
            isLaunched = true;
        }

        



    }
    
    public void ResetBall()
    {
        launchSpeed = 25f; //Default the launchspeed so the player can't just catch and release at full power.
        currentCharge = 0f;
        isLaunched = false;
        rb.isKinematic = true;
        rb.velocity = Vector2.zero;
        transform.position = paddle.position + offsetFromPaddle;
        chargedEffect.SetActive(false);
        StartCoroutine(chargeSoundDelay());



    }

    IEnumerator chargeSoundDelay()
    {
        // Wait for half seconds
        yield return new WaitForSeconds(.7f);
        audioSource.PlayOneShot(chargeSound, 4.0f);



    }

    public void ChargeBall()
    {
        if (isLaunched == false)
        {
            currentCharge += chargeRate * Time.deltaTime;
            currentCharge = Mathf.Clamp(currentCharge, 0f, maxCharge);

            // Scale launch speed with charge (10 at min, 20 at max)
            launchSpeed = Mathf.Lerp(25f, 45f, currentCharge / maxCharge);
            
        }
        if (currentCharge >= maxCharge)
        {
            chargedEffect.SetActive(true);
            //Debug.Log("Ball Charged!!!");
            
        }
        
    }

    void Update()
    {
        // If not launched, stick ball to paddle
        if (!isLaunched)
        {
            // Keep ball stuck to paddle
            transform.position = paddle.position + offsetFromPaddle;
            //Charge the ball!
            ChargeBall();
            // Check for mouse button press
            if (Input.GetMouseButtonDown(0))
            {
                audioSource.PlayOneShot(throwSound, 3.0f);
                LaunchBall();
                
            }
        }
        else
        {
            
            // Calculate how much time has passed since launch
            float timeSinceLaunch = Time.time - launchTime;
            float t = Mathf.Clamp01(timeSinceLaunch / decayDuration);

            // Lerp ballSpeed down over time
            ballSpeed = Mathf.Lerp(launchSpeed, minBallSpeed, t);

            if (ballSpeed < 35f)
            {
                chargedEffect.SetActive(false);
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
            ResetBall();
        }


    }
    
}