using UnityEngine;

public class GemManager : MonoBehaviour
{
    public GameObject gemPrefab;    // Assign the gem prefab in the inspector
    public int totalGems = 8;       // Total number of gems to collect
    public float spawnRadius = 5000f;  // Radius within which gems will spawn
    private int collectedGems = 0;     // Track collected gems

    void Start()
    {
        SpawnGems();
    }

    void SpawnGems()
    {
        for (int i = 0; i < totalGems; i++)
        {
            Vector3 spawnPosition = GetRandomPosition();
            Instantiate(gemPrefab, spawnPosition, Quaternion.identity);
        }
    }

    Vector3 GetRandomPosition()
    {
        // Generate a random position within a circular area centered on the GemManager’s position
        Vector2 randomCircle = Random.insideUnitCircle * spawnRadius;
        return new Vector3(randomCircle.x, 0, randomCircle.y) + transform.position;
    }

    public void CollectGem()
    {
        collectedGems++;
        Debug.Log("Gems collected: " + collectedGems + "/" + totalGems);

        // Check for win condition
        if (collectedGems >= totalGems)
        {
            WinGame();
        }
    }

    private void WinGame()
    {
        Debug.Log("Congratulations! You've collected all gems and won the game!");
        // Additional win logic (e.g., show win screen) can be added here
    }
}
