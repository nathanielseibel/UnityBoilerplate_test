using UnityEngine;

public class BrickSpawner : MonoBehaviour
{
    // Prefab of the brick to spawn
    [SerializeField] private GameObject brickPrefab;
    // Prefab of a tanky brick to spawn
    [SerializeField] private GameObject tankyBrickPrefab;
    // Prefab of a super tanky brick to spawn
    [SerializeField] private GameObject superTankyBrickPrefab;
    // Prefab of a speed brick to spawn
    [SerializeField] private GameObject speedBrickPrefab;

    // Radius to check for overlapping bricks
    public float checkRadius = 0.5f; 

    // Check if there's already a brick at this position
    private bool IsPositionClear()
    {
        // Check for overlapping colliders at spawn position
        Collider[] colliders = Physics.OverlapSphere(transform.position, checkRadius);

        // If we find any colliders with brick tags, position is not clear
        foreach (Collider col in colliders)
        {
            if (col.CompareTag("Brick") || col.CompareTag("TankyBrick") || col.CompareTag("SpeedBrick"))
            {
                return false; // Position occupied
            }
        }

        return true; // Position is clear
    }

    // Spawn a brick at spawner's position
    public void SpawnBrickHere()
    {
        // Check if position is clear before spawning
        if (IsPositionClear())
        {
            Instantiate(brickPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.Log("Spawn blocked - brick already at this position");
        }
    }

    // Spawn a tanky brick at spawner's position
    public void SpawnTankyBrickHere()
    {
        // Check if position is clear before spawning
        if (IsPositionClear())
        {
            Instantiate(tankyBrickPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.Log("Spawn blocked - brick already at this position");
        }
    }

    // Spawn a super tanky brick at spawner's position
    public void SpawnSuperTankyBrickHere()
    {
        // Check if position is clear before spawning
        if (IsPositionClear())
        {
            Instantiate(superTankyBrickPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.Log("Spawn blocked - brick already at this position");
        }
    }

    // Spawn a speed brick at spawner's position
    public void SpawnSpeedBrickHere()
    {
        // Check if position is clear before spawning
        if (IsPositionClear())
        {
            Instantiate(speedBrickPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.Log("Spawn blocked - brick already at this position");
        }
    }

}
