using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // List to hold spawn points for enemies
    public static List<Transform> spawnPoints = new List<Transform>();

    // Prefab of the enemy to be spawned
    public GameObject enemyPrefab;

    // Container to hold spawned enemies
    public GameObject enemyContainer;

    // Number of enemies to spawn in each burst
    public float enemyBurstCount = 6f;

    // Time interval between enemy spawns
    public float spawnTime = 1f;

    // Reference to the previously used spawn location
    private Transform oldLocation;

    // Current spawn location
    private Transform location;

    // Time tracker for spawn timing
    private float updateTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        // Populate the spawnPoints list with child transforms of this object
        foreach (Transform child in transform)
        {
            spawnPoints.Add(child);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Check if it's time to spawn an enemy
        if (Time.time > updateTime)
        {
            // Update the next spawn time
            updateTime = Time.time + spawnTime;

            // Call the method to spawn an enemy
            SpawnEnemy();
        }
    }

    // Method to spawn an enemy
    public void SpawnEnemy()
    {
        // Create a copy of the spawnPoints list to avoid modifying the original list
        List<Transform> spawnPointsCopy = new List<Transform>(spawnPoints);

        // Check if there's still room for more enemies
        if (enemyContainer.transform.childCount < enemyBurstCount)
        {
            // Choose a random spawn point
            do
            {
                location = spawnPointsCopy[Random.Range(0, spawnPointsCopy.Count)];
            } while (location == oldLocation); // Ensure the new location is different from the previous one

            // Update the old location to the current one
            oldLocation = location;

            // Instantiate the enemy prefab at the chosen location with its rotation
            var enemyInstance = Instantiate(enemyPrefab, location.position, location.rotation);

            // Set the enemy's parent to the enemyContainer
            enemyInstance.transform.SetParent(enemyContainer.transform);
        }
    }
}
