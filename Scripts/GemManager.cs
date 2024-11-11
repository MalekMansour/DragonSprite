using UnityEngine;

public class GemManager : MonoBehaviour
{
    public GameObject gemPrefab;  // Assign the gem prefab in the inspector
    public int totalGems = 8;     // Total number of gems to collect
    public Transform[] spawnPoints;  // Array of spawn points for the gems
    private int collectedGems = 0;   // Track collected gems

    void Start()
    {
        // Check if we have enough spawn points
        if (spawnPoints.Length < totalGems)
        {
            Debug.LogError("Not enough spawn points for the gems!");
            return;
        }

        // Spawn the gems at specified spawn points
        for (int i = 0; i < totalGems; i++)
        {
            Instantiate(gemPrefab, spawnPoints[i].position, Quaternion.identity);
        }
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
