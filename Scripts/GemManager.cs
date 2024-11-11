using UnityEngine;
using TMPro;

public class GemManager : MonoBehaviour
{
    public GameObject gemPrefab;         // Assign the original gem prefab in the inspector
    public int totalGems = 8;            // Total gems to spawn and collect
    public float spawnRadius = 5000f;    // Radius within which to spawn gems
    public TextMeshProUGUI gemCounter;   // Text to show the number of gems collected
    public GameObject winScreen;         // Assign the win screen UI in the inspector

    private int collectedGems = 0;       // Track collected gems

    void Start()
    {
        SpawnGems();
        UpdateGemCounter();
        winScreen.SetActive(false);      // Hide win screen initially
    }

    void SpawnGems()
    {
        // Spawn the gemPrefab `totalGems` times
        for (int i = 0; i < totalGems; i++)
        {
            Vector3 spawnPosition = GetRandomGroundPosition();
            Instantiate(gemPrefab, spawnPosition, Quaternion.identity);
        }
    }

    Vector3 GetRandomGroundPosition()
    {
        // Generate a random position within the spawn radius and retain the gem's original Y position
        Vector2 randomCircle = Random.insideUnitCircle * spawnRadius;
        Vector3 randomPosition = new Vector3(randomCircle.x, gemPrefab.transform.position.y, randomCircle.y) + transform.position;
        return randomPosition;
    }

    public void CollectGem()
    {
        collectedGems++;
        UpdateGemCounter();

        if (collectedGems >= totalGems)
        {
            WinGame();
        }
    }

    private void UpdateGemCounter()
    {
        // Update the TextMeshPro counter with collected gems
        gemCounter.text = $"Gems: {collectedGems} / {totalGems}";
    }

    private void WinGame()
    {
        Debug.Log("Congratulations! You've collected all gems and won the game!");
        winScreen.SetActive(true);      // Display the win screen
    }
}
