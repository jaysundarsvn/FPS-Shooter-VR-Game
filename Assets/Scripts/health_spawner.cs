using System.Collections;
using UnityEngine;

public class Health_spawner : MonoBehaviour
{
    public GameObject healthPickupPrefab; // The prefab for the health pickup
    public Transform[] spawnPoints; // Array of spawn points where health pickups can spawn
    public float spawnInterval = 2f; // Interval between each spawn

    private bool canSpawn = true; // Flag to control spawning

    // Start is called before the first frame update
    void Start()
    {
        // Start spawning health pickups when the game starts
        StartCoroutine(SpawnHealthPickups());
    }

    // Coroutine to continuously spawn health pickups
    IEnumerator SpawnHealthPickups()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval); // Wait for the specified interval

            // Spawn a health pickup only if canSpawn flag is true
            if (canSpawn)
            {
                // Choose a random spawn point
                int spawnIndex = Random.Range(0, spawnPoints.Length);
                Transform spawnPoint = spawnPoints[spawnIndex];

                // Spawn the health pickup at the chosen spawn point
                Instantiate(healthPickupPrefab, spawnPoint.position, Quaternion.identity);
                canSpawn = false; // Prevent spawning until the pickup is collected
            }
        }
    }

    // Function to be called when a health pickup is collected
    public void HealthPickupCollected()
    {
        canSpawn = true; // Allow spawning again
    }
}
