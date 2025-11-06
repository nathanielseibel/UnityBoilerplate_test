using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RaftScript : MonoBehaviour
{
    //For bomb
    public GameObject bombPrefab;
    public float throwForce = 10f;
    public float throwHeight = 10f;
    private bool hasThrownBomb = false;
    

    //initialize health of brick
    private int health = 1;
    //public getter for health
    public int GetHealth()
    {
        return health;
    }
    //Take away health
    public void TakeDamage()
    {
        health--;
        
    }

    //if brick is tag "brick", it has 1 health
    private void Awake()
    {
        //if brick is tag "brick", it has 1 health
        if (gameObject.tag == "Brick")
        {
            health = 1;
        }
        else if (gameObject.tag == "TankyBrick")
        {
            health = 3;
        }
        else if (gameObject.tag == "SuperTankyBrick")
        {
            health = 5;
        }
        else if (gameObject.tag == "SpeedBrick")
        {
            health = 2;
        }

    }

    private int scoreValue;

    public float moveDistance = 1f; // How far to move down each time
    public float moveInterval = 1.5f; // Time between moves in seconds

    private float moveTimer = 0f;

    private bool canMove = true;

    //get spawner manager
    private SpawnManager spawnManager;

    //get score manager
    private ScoreManager scoreManager;



    void Start()
    {
        //get box collider
        //This line retrieves the BoxCollider component attached to the same GameObject as this script and assigns it to the boxCol variable.
        // The BoxCollider is used for collision detection when the brick attempts to move down.
        // By accessing the BoxCollider, the script can determine the size and shape of the brick for accurate collision checks.
        // This is essential for ensuring that the brick only moves down when there is enough space and does not overlap with other objects.
        // It allows the MoveBrickDown() method to use the collider's dimensions to perform overlap checks against the collision mask.
        // In summary, this line is crucial for enabling proper collision detection and movement behavior for the brick in the game.
        // Without it, the brick would not be able to check for collisions when moving down.
        boxCol = GetComponent<BoxCollider>();


        //initialize collision mask
        //The mask is used to filter which layers the brick will check for collisions with when moving down.
        // By setting the mask to only include the "Brick" and "Ground" layers, the brick will only consider collisions with objects on these layers.
        // This is important for ensuring that the brick only stops moving when it encounters other bricks or the ground, and ignores other objects in the scene that are not relevant to its movement.

        // Set the mask to only detect "Brick" and "Ground" layers
        collisionMask = LayerMask.GetMask("Brick", "Ground");

        //if a regular brick is destroyed, minus one from total bricks alive
        if (spawnManager == null)
        {
            spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        } else
        {
            Debug.Log("Spawn Manager found at start");
        }

        if (scoreManager == null)
        {
            scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();

        }
        else
        {
            Debug.Log("Score Manager found at start");
        }

    }
    void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is with a ball
        // Reduce health and check for destruction
        // Debug log for collision
        if (collision.gameObject.tag == "Ball")
        {
            health--;
            if (health <= 0)
            {
                Destroy(gameObject);
                Debug.Log("Brick destroyed on collision with ball.");
            }
        }

        // Check if the collision is with a bomb
        // Destroy the brick immediately
        // Debug log for collision
        if (collision.gameObject.tag == "Bomb")
        {
             Destroy(gameObject);
             Debug.Log("Brick destroyed on collision with bomb.");

            // Destroy the bomb as well
            Destroy(collision.gameObject);
        }
    }

    //Implement addscore() from score manager when brick is destroyed
    private void OnDestroy()
    {
        // Set score value based on the brick's tag
        if (gameObject.CompareTag("Brick"))
        {
            scoreValue = 50; // Regular brick
            spawnManager.SubtractFromTotal();
            //minus one from max regular bricks
            spawnManager.SubtractFromRegularBricks();
            Debug.Log("Regular Brick Destroyed. Total Bricks Alive: " + spawnManager.GetTotalBricksAlive());
        }
        else if (gameObject.CompareTag("TankyBrick"))
        {
            scoreValue = 250; // Tanky brick
            //tank brick destroyed
            spawnManager.SubtractFromTotal();
            //minus one from max tanky bricks
            spawnManager.SubtractFromTankyBricks();
            Debug.Log("Tanky Brick Destroyed. Total Bricks Alive: " + spawnManager.GetTotalBricksAlive());
        }
        else if (gameObject.CompareTag("SuperTankyBrick"))
        {
            scoreValue = 500; // Super Tanky brick
            //super tank brick destroyed
            spawnManager.SubtractFromTotal();
            //minus one from max super tanky bricks
            spawnManager.SubtractFromSuperTankyBricks();
            Debug.Log("Super Tanky Brick Destroyed. Total Bricks Alive: " + spawnManager.GetTotalBricksAlive());
        }
        else if (gameObject.CompareTag("SpeedBrick"))
        {
            scoreValue = 100; // Speed brick
            //speed brick destroyed
            spawnManager.SubtractFromTotal();
            //minus one from max speed bricks
            spawnManager.SubtractFromSpeedBricks();
            Debug.Log("Speed Brick Destroyed. Total Bricks Alive: " + spawnManager.GetTotalBricksAlive());
        }
        else
        {
                       scoreValue = 0; // Unknown brick type
        }

        scoreManager.AddScore(scoreValue);
        
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

        //if the brick reaches 2 on the y axis, prevent it from going further down
        if (transform.position.y <= 4f)
        {
            canMove = false;
            Vector3 pos = transform.position;
            pos.y = 4f;
            transform.position = pos;
        }

        //if the brick goes off the top of the screen, destroy it
        if (transform.position.y > 30)
        {
            Destroy(gameObject);
        }

        //If the brick goes off the bottom of the screen, destroy it
        if (transform.position.y < -3.0f)
        {
            Destroy(gameObject);
        }


        //For bomb throwing
        // Check if this brick has reached the throw height and hasn't thrown a bomb yet
        if (!hasThrownBomb && transform.position.y <= throwHeight)
        {
            // Check if this object has the correct tag
            if (gameObject.CompareTag("Brick") || gameObject.CompareTag("SpeedBrick"))
            {
                ThrowBomb();
                ResetThrow();
            }
        }
    }

    void ThrowBomb()
    {
        // Instantiate the bomb at the brick's position
        GameObject bomb = Instantiate(bombPrefab, transform.position, Quaternion.identity);

        // Get the Rigidbody component and apply downward force
        Rigidbody rb = bomb.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(Vector3.down * throwForce, ForceMode.Impulse);
        }

        // Mark that we've thrown the bomb so we don't throw multiple
        hasThrownBomb = true;

        Debug.Log(gameObject.name + " threw a bomb!");
    }

    // Optional: Reset if you want the brick to throw again
    public void ResetThrow()
    {
        //wait 3 seconds before resetting
        if (hasThrownBomb) {
            StartCoroutine(ResetThrowCoroutine());
        }
    }

    // Coroutine to reset bomb throw after a delay
    private IEnumerator ResetThrowCoroutine()
    {
        yield return new WaitForSeconds(3f);

        hasThrownBomb = false;
    }


    // Movement and Collision Detection
    [SerializeField] private LayerMask collisionMask;
    private BoxCollider boxCol;

    private void MoveBrickDown()
    {
        if (!canMove) return;

        // Check the position we WANT to move to
        Vector3 nextPosition = transform.position + Vector3.down * moveDistance;

        // Check if that position would overlap anything
        Collider[] hits = Physics.OverlapBox(
            nextPosition,
            boxCol.size / 2f * 0.95f,
            transform.rotation,
            collisionMask
        );

        if (hits.Length > 0)
        {
            // Can't move - we'd hit something. Stop here.
            canMove = false;
            enabled = false; // Disable script to save performance
        }
        else
        {
            // Safe to move
            transform.position = nextPosition;
        }
    }









}



