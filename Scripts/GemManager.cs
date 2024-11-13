using UnityEngine;
using TMPro;

public class GemManager : MonoBehaviour
{
    public GameObject gemPrefab;         // Original gem prefab
    public int totalGems = 8;            // Total gems to spawn and collect
    public float spawnRadius = 5000f;    // Radius within which to spawn gems
    public TextMeshProUGUI gemCounter;   // Text for the soul counter
    public GameObject winScreen;         // Win screen UI

    private int collectedGems = 0;       // Track collected gems

    void Start()
    {
        SpawnGems();
        UpdateGemCounter();
        winScreen.SetActive(false);      // Hide win screen initially

        // Lock the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void SpawnGems()
    {
        for (int i = 0; i < totalGems; i++)
        {
            Vector3 spawnPosition = GetRandomGroundPosition();
            Instantiate(gemPrefab, spawnPosition, Quaternion.identity);
        }
    }

    Vector3 GetRandomGroundPosition()
    {
        Vector2 randomCircle = Random.insideUnitCircle * spawnRadius;
        Vector3 randomPosition = new Vector3(randomCircle.x, 0, randomCircle.y) + transform.position;

        // Raycast to find ground level at random position
        if (Physics.Raycast(randomPosition + Vector3.up * 1000, Vector3.down, out RaycastHit hit, 2000f))
        {
            return hit.point;
        }

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
        gemCounter.text = $"Soul Counter: {collectedGems} / {totalGems}";
    }

    private void WinGame()
    {
        Debug.Log("Congratulations! You've collected all souls and won the game!");
        winScreen.SetActive(true);

        // Unlock the cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
