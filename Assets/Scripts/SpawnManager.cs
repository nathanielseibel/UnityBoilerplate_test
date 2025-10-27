using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Array to hold all spawners in the scene
    private BrickSpawner[] brickSpawners;

    // Time tracking for spawning
    private float spawnTimer = 0f;
    private float spawnInterval = 1f; // Spawn regular bricks every X seconds

    // Tanky brick tracking
    private float tankySpawnTimer = 0f;
    private float tankySpawnInterval = 2f; // Spawn tanky bricks every X seconds
    private int maxTankyBricks = 5;

    // Tanky brick tracking
    private float superTankySpawnTimer = 0f;
    private float superTankySpawnInterval = 2f; // Spawn super tanky bricks every X seconds
    private int maxSuperTankyBricks = 3;

    // Speed brick tracking
    private float speedSpawnTimer = 0f;
    private float speedSpawnInterval = 1f; // Spawn speed bricks every 2 seconds after a tanky brick
    private bool shouldSpawnSpeedBrick = false;

    private void Start()
    {
        // Find all GameObjects with the "spawner" tag
        GameObject[] spawnerObjects = GameObject.FindGameObjectsWithTag("spawner");

        // Get the BrickSpawner component from each spawner object
        brickSpawners = new BrickSpawner[spawnerObjects.Length];
        for (int i = 0; i < spawnerObjects.Length; i++)
        {
            brickSpawners[i] = spawnerObjects[i].GetComponent<BrickSpawner>();
        }

        // Optional: Log how many spawners were found
        Debug.Log($"Found {brickSpawners.Length} spawner(s)");
    }

    private void Update()
    {
        // Check if we have any spawners
        if (brickSpawners == null || brickSpawners.Length == 0)
            return;

        // === Regular Brick Spawning ===
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            spawnTimer = 0f;

            // Pick a random spawner
            int randomIndex = Random.Range(0, brickSpawners.Length);
            BrickSpawner selectedSpawner = brickSpawners[randomIndex];

            // Spawn brick from the selected spawner
            if (selectedSpawner != null)
            {
                selectedSpawner.SpawnBrickHere();
            }
        }

        // === Tanky Brick Spawning ===
        tankySpawnTimer += Time.deltaTime;

        if (tankySpawnTimer >= tankySpawnInterval)
        {
            tankySpawnTimer = 0f;

            // Count how many tanky bricks currently exist
            int tankyBrickCount = GameObject.FindGameObjectsWithTag("TankyBrick").Length;

            // Only spawn if we have less than the max
            if (tankyBrickCount < maxTankyBricks)
            {
                // Pick a random spawner
                int randomIndex = Random.Range(0, brickSpawners.Length);
                BrickSpawner selectedSpawner = brickSpawners[randomIndex];

                // Spawn tanky brick from the selected spawner
                if (selectedSpawner != null)
                {
                    selectedSpawner.SpawnTankyBrickHere();
                    Debug.Log($"Spawned tanky brick. Total: {tankyBrickCount + 1}");

                    // Trigger speed brick spawning
                    shouldSpawnSpeedBrick = true;
                    speedSpawnTimer = 0f; // Reset the timer
                }
            }
        }

        // === Super Tanky Brick Spawning ===
        tankySpawnTimer += Time.deltaTime;

        if (tankySpawnTimer >= tankySpawnInterval)
        {
            tankySpawnTimer = 0f;

            // Count how many tanky bricks currently exist
            int superTankyBrickCount = GameObject.FindGameObjectsWithTag("SuperTankyBrick").Length;

            // Only spawn if we have less than the max
            if (superTankyBrickCount < maxSuperTankyBricks)
            {
                // Pick a random spawner
                int randomIndex = Random.Range(0, brickSpawners.Length);
                BrickSpawner selectedSpawner = brickSpawners[randomIndex];

                // Spawn tanky brick from the selected spawner
                if (selectedSpawner != null)
                {
                    selectedSpawner.SpawnSuperTankyBrickHere();
                    Debug.Log($"Spawned super tanky brick. Total: {superTankyBrickCount + 1}");
                }
            }
        }

        // === Speed Brick Spawning ===
        if (shouldSpawnSpeedBrick)
        {
            speedSpawnTimer += Time.deltaTime;

            if (speedSpawnTimer >= speedSpawnInterval)
            {
                speedSpawnTimer = 0f;
                shouldSpawnSpeedBrick = false; // Only spawn once per tanky brick

                // Pick a random spawner
                int randomIndex = Random.Range(0, brickSpawners.Length);
                BrickSpawner selectedSpawner = brickSpawners[randomIndex];

                // Spawn speed brick from the selected spawner
                if (selectedSpawner != null)
                {
                    selectedSpawner.SpawnSpeedBrickHere();
                    Debug.Log("Spawned speed brick after tanky brick");
                }
            }
        }
    }
}
