using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RaftScript : MonoBehaviour
{
    
    
    private int health = 1;

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
        boxCol = GetComponent<BoxCollider>();

        // Set the mask to only detect "Brick" and "Ground" layers
        collisionMask = LayerMask.GetMask("Brick", "Ground");

        //if a regular brick is destroyed, minus one from total bricks alive
        if (spawnManager == null)
        {
            spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        }

    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            health--;
            if (health <= 0)
            {
                Destroy(gameObject);
            }
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
        else if (gameObject.CompareTag("PowerUp"))
        {
            scoreValue = 200; // Power ups
        }
        else if (gameObject.CompareTag("Bomb"))
        {
            scoreValue = 100; // Bomb
        }
        else
        {
            scoreValue = 10; // Default value
        }

        if (scoreManager == null)
        {
            scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
            //Debug.Log("Score Manager found");
            //throw new error if score manager not found
            if (scoreManager == null)
            {
                Debug.LogError("Score Manager not found!");
            }

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
        if (transform.position.y <= 2f)
        {
            canMove = false;
            Vector3 pos = transform.position;
            pos.y = 2f;
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
    }

    
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
