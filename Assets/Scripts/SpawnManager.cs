using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Array to hold all spawners in the scene
    private BrickSpawner[] brickSpawners;

    //keep track of total bricks alive
    private static int totalBricksAlive = 0;

    //keep track of last spawner used
    private int lastSpawnerIndex = -1;

    //plus one to total bricks alive
    public int AddToTotal()
    {
        return totalBricksAlive++;
    }

    // Time tracking for spawning
    private float spawnTimer = 4f;
    private float spawnInterval = 4f; // Spawn regular bricks every X seconds
    private int maxRegularBricks = 0;

    //plus one to max regular bricks
    public int AddToRegularBricks()
    {
        return maxRegularBricks++;
    }

    // Tanky brick tracking
    private float tankySpawnTimer = 4f;
    private float tankySpawnInterval = 4f; // Spawn tanky bricks every X seconds
    //keep track of max tanky bricks
    private int maxTankyBricks = 0;

    //plus one to max tanky bricks
    public int AddToTankyBricks()
    {
        return maxTankyBricks++;
    }


    // Super Tanky brick tracking
    private float superTankySpawnTimer = 4f;
    private float superTankySpawnInterval = 4f; // Spawn super tanky bricks every X seconds
    private int maxSuperTankyBricks = 0;

    //plus one to max super tanky bricks
    public int AddToSuperTankyBricks()
    {
        return maxSuperTankyBricks++;
    }

    // Speed brick tracking
    private float speedSpawnTimer = 4f;
    private float speedSpawnInterval = 4f; // Spawn speed bricks every x seconds
    private int maxSpeedBricks = 0;

    //plus one to max speed bricks
    public int AddToSpeedBricks()
    {
        return maxSpeedBricks++;
    }

    //Get max speed bricks
    public int GetMaxSpeedBricks()
    {
        return maxSpeedBricks;
    }


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

    private int GetRandomSpawnerIndex()
    {
        if (brickSpawners.Length <= 1)
            return 0;

        int newIndex;
        do
        {
            newIndex = Random.Range(0, brickSpawners.Length);
        }
        while (newIndex == lastSpawnerIndex);

        lastSpawnerIndex = newIndex;
        return newIndex;
    }

    private void Update()
    {
        // Check if we have any spawners
        if (brickSpawners == null || brickSpawners.Length == 0)
        {
            return;
        }

        // === Regular Brick Spawning ===
        spawnTimer += Time.deltaTime;
        tankySpawnTimer += Time.deltaTime;
        superTankySpawnTimer += Time.deltaTime;
        speedSpawnTimer += Time.deltaTime;

        //if total bricks alive is less than 48, spawn more bricks
        if (totalBricksAlive < 48 && spawnTimer >= spawnInterval)
        {
            spawnTimer = 0f;

            //spawn a regular brick at a different spawner than last time
            int randomIndex = GetRandomSpawnerIndex();
            brickSpawners[randomIndex].SpawnBrickHere();
            AddToTotal();
            AddToRegularBricks();
            Debug.Log("Total bricks alive: " + totalBricksAlive);
            Debug.Log("Max regular bricks: " + maxRegularBricks);
        }

        //spawn a tanky brick if under the limit
        if (totalBricksAlive < 48 && maxTankyBricks < 8 && tankySpawnTimer >= tankySpawnInterval)
        {
            tankySpawnTimer = 0f;
            int randomTankyIndex = GetRandomSpawnerIndex();
            brickSpawners[randomTankyIndex].SpawnTankyBrickHere();
            AddToTotal();
            AddToTankyBricks();
            Debug.Log("Total bricks alive: " + totalBricksAlive);
            Debug.Log("Max tanky bricks: " + maxTankyBricks);
        }

        //spawn a super tanky brick if under the limit
        if (totalBricksAlive < 48 && maxSuperTankyBricks < 4 && superTankySpawnTimer >= superTankySpawnInterval)
        {
            superTankySpawnTimer = 0f;
            int randomSuperTankyIndex = GetRandomSpawnerIndex();
            brickSpawners[randomSuperTankyIndex].SpawnSuperTankyBrickHere();
            AddToTotal();
            AddToSuperTankyBricks();
            Debug.Log("Total bricks alive: " + totalBricksAlive);
            Debug.Log("Max super tanky bricks: " + maxSuperTankyBricks);
        }

        //spawn a speed brick if under the limit
        if (totalBricksAlive < 48 && maxSpeedBricks < 12 && speedSpawnTimer >= speedSpawnInterval)
        {
            speedSpawnTimer = 0f;
            int randomSpeedIndex = GetRandomSpawnerIndex();
            brickSpawners[randomSpeedIndex].SpawnSpeedBrickHere();
            AddToTotal();
            AddToSpeedBricks();
            Debug.Log("Total bricks alive: " + totalBricksAlive);
            Debug.Log("Max speed bricks: " + maxSpeedBricks);
        }
    }
}
