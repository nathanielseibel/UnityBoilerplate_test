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


    // Spawn a brick at spawner's position
    public void SpawnBrickHere()
    {
        Instantiate(brickPrefab, transform.position, Quaternion.identity);
    }

    // Spawn a tanky brick at spawner's position
    public void SpawnTankyBrickHere()
    {
        Instantiate(tankyBrickPrefab, transform.position, Quaternion.identity);
    }

    public void SpawnSuperTankyBrickHere()
    {
        Instantiate(superTankyBrickPrefab, transform.position, Quaternion.identity);
    }

    public void SpawnSpeedBrickHere()
    {
        Instantiate(speedBrickPrefab, transform.position, Quaternion.identity);
    }

}
