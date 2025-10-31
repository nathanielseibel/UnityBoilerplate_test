using UnityEngine;

public class BrickBehavior : MonoBehaviour
{
    private int scoreValue;

    public float moveDistance = 1f; // How far to move down each time
    public float moveInterval = 1.5f; // Time between moves in seconds

    private float moveTimer = 0f;

    private bool canMove = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Set score value based on the brick's tag
        if (gameObject.CompareTag("Brick"))
        {
            scoreValue = 50; // Regular brick
        }
        else if (gameObject.CompareTag("TankyBrick"))
        {
            scoreValue = 250; // Tanky brick
        }
        else if (gameObject.CompareTag("SuperTankyBrick"))
        {
            scoreValue = 500; // Super Tanky brick
        }
        else if (gameObject.CompareTag("SpeedBrick"))
        {
            scoreValue = 100; // Speed brick
        }
        else if (gameObject.CompareTag("PowerUp"))
        {
            scoreValue = 200; // Power ups
        }
        else if (gameObject.CompareTag("ReboundBomb"))
        {
            scoreValue = 100; // Rebound Bomb
        }
        else
        {
            scoreValue = 10; // Default value
        }

    }

    // Update is called once per frame
    void Update()
    {
        // Increment the timer
        moveTimer += Time.deltaTime;

        // Check if it's time to move
        if (moveTimer >= moveInterval)
        {
            // Reset timer
            moveTimer = 0f;

            // Move the brick down
            MoveBrickDown();
        }

        //if the brick reaches 0 on the y axis, prevent it from going further down
        if (transform.position.y <= 0.5f)
        {
            Vector3 pos = transform.position;
            pos.y = 0.5f;
            transform.position = pos;
        }

        //if the brick goes off the top of the screen, destroy it
        if (transform.position.y > 25)
        {
            Destroy(gameObject);
        }

        //If the brick goes off the bottom of the screen, destroy it
        if (transform.position.y < -15.0f)
        {
            Destroy(gameObject);
        }
    }

    private void MoveBrickDown()
    {
        // Move down by the specified distance
        if (canMove)
        { 
        transform.position += Vector3.down * moveDistance;
        }
        //check if the brick will collide with another brick in the next move
        if (Physics.Raycast(transform.position, Vector3.down, moveDistance))
        {
            //stop all movement, velocity
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.linearVelocity = Vector3.zero;
            //stop calling move brick down
            canMove = false;
        }

    }

    //if this brick touches another brick
    private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Brick") || collision.gameObject.CompareTag("TankyBrick") || collision.gameObject.CompareTag("SpeedBrick"))
            {
                //stop all movement, velocity
                Rigidbody rb = GetComponent<Rigidbody>();
                rb.linearVelocity = Vector3.zero;

                //stop calling move brick down
                canMove = false;
            }
        }

    private void OnDestroy()
    {
        // Add score when brick is destroyed
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.AddScore(scoreValue);
        }
    }


}
