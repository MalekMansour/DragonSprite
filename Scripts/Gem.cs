using UnityEngine;
using TMPro;

public class GemManager : MonoBehaviour
{
    public GameObject gemPrefab;        // Assign the original gem prefab in the inspector
    public int totalGems = 8;           // Total gems to spawn and collect
    public float spawnRadius = 5000f;   // Radius within which to spawn gems
    public TextMeshProUGUI gemCounter;  // Text to show the number of gems collected

    private int collectedGems = 0;      // Track collected gems

    void Start()
    {
        // Spawn gems around the map
        SpawnGems();

        // Initialize gem counter display
        UpdateGemCounter();
    }

    void SpawnGems()
    {
        // Duplicate the gemPrefab `totalGems` times
        for (int i = 0; i < totalGems; i++)
        {
            Vector3 spawnPosition = GetRandomGroundPosition();
            Instantiate(gemPrefab, spawnPosition, Quaternion.identity);
        }
    }

    Vector3 GetRandomGroundPosition()
    {
        // Generate a random position within a circular area centered on the GemManager’s position
        Vector2 randomCircle = Random.insideUnitCircle * spawnRadius;
        Vector3 randomPosition = new Vector3(randomCircle.x, 0, randomCircle.y) + transform.position;

        // Adjust Y position to the ground level (Y=0 or the specific ground level)
        randomPosition.y = gemPrefab.transform.position.y; // Use the Y position of the original gem prefab
        return randomPosition;
    }

    public void CollectGem()
    {
        collectedGems++;
        UpdateGemCounter();

        // Check for win condition
        if (collectedGems >= totalGems)
        {
            WinGame();
        }
    }

    private void UpdateGemCounter()
    {
        // Update the TextMeshPro counter with collected gems
        gemCounter.text = "Gems: " + collectedGems + " / " + totalGems;
    }

    private void WinGame()
    {
        Debug.Log("Congratulations! You've collected all gems and won the game!");
        // Additional win logic (e.g., show win screen) can be added here
    }
}
