using UnityEngine;

public class BrickBehavior : MonoBehaviour
{



    // Speed at which the brick moves down the screen
    public float speed = 1.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //The brick moves down the screen at a constant speed using the speed variable
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        //If the brick goes off the bottom of the screen, destroy it
        if (transform.position.y < -10.0f)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        // Clamp the velocity to a maximum speed
        if (rb.linearVelocity.magnitude > 10f) // Max speed of 10
        {
            rb.linearVelocity = rb.linearVelocity.normalized * 10f;
        }

        //if the brick changes speed, reset its velocity to match the speed variable
        rb.linearVelocity = Vector3.down * speed;
    }

    //check if this object has tag "SpeedBrick" and if so, increase its speed
    private void Awake()
    {
        if (gameObject.CompareTag("SpeedBrick"))
        {
            speed *= 2.0f; //double the speed
        }
    }

    //check if this object has tag "TankyBrick" and if so, half it's speed
    private void OnEnable()
    {
        if (gameObject.CompareTag("TankyBrick"))
        {
            //constantly set speed to half every frame over and over again
            speed *= 0.5f; //half the speed

        }
    }

    //if this brick touches another brick, attach to it without setting a parent
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Brick") || collision.gameObject.CompareTag("TankyBrick") || collision.gameObject.CompareTag("SpeedBrick"))
        {
            // Attach to the other brick by setting its position relative to the other brick
            Vector3 offset = transform.position - collision.transform.position;
            transform.position = collision.transform.position + offset;

            //set speed to 1 for both bricks
            speed = 1.0f;

        }
    }


}
