using UnityEngine;

public class EnemyMultiplier : MonoBehaviour
{
    public GameObject enemyPrefab;     
    public int numberOfEnemies = 10;    // Number of enemies to spawn
    public float spawnRange = 500f;      // Range for enemy spawning

    private bool hasSpawned = false;    

    void Start()
    {
        // Ensure the enemyPrefab is assigned in the inspector
        if (enemyPrefab == null)
        {
            Debug.LogError("Enemy prefab is not assigned in the inspector.");
            return;
        }

        // If the enemies haven't been spawned yet, spawn them
        if (!hasSpawned)
        {
            SpawnEnemies();
            hasSpawned = true;  // Prevent further spawning after the first time
        }
    }

    void SpawnEnemies()
    {
        // Debug log to confirm spawning starts
        Debug.Log("Spawning " + numberOfEnemies + " enemies.");

        for (int i = 0; i < numberOfEnemies; i++)
        {
            // Generate a random position for each enemy
            Vector3 spawnPosition = new Vector3(
                Random.Range(-spawnRange, spawnRange),
                0,  // Ground level (you can change this if necessary)
                Random.Range(-spawnRange, spawnRange)
            );

            // Instantiate the enemy prefab at the spawn position
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

            // Debug log to confirm each enemy's spawn position
            Debug.Log("Enemy spawned at: " + spawnPosition);
        }
    }
}
