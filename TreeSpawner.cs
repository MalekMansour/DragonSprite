using UnityEngine;

public class TreeSpawner : MonoBehaviour
{
    public GameObject[] treePrefabs;    // Assign tree1, tree2, tree3 from the hierarchy here
    public int numberOfTrees = 100;     // Total number of trees to spawn
    public float spawnRange = 50f;      // Range for tree spawning
    public Vector3 treeScale = new Vector3(1f, 1f, 1f);  // Uniform scale for all trees

    void Start()
    {
        // Check if any trees are assigned
        if (treePrefabs == null || treePrefabs.Length == 0)
        {
            Debug.LogWarning("No tree prefabs assigned in the inspector.");
            return;
        }

        // Spawn the specified number of trees
        SpawnTrees();
    }

    void SpawnTrees()
    {
        for (int i = 0; i < numberOfTrees; i++)
        {
            // Pick a random tree prefab
            GameObject treePrefab = treePrefabs[Random.Range(0, treePrefabs.Length)];

            // Generate a random position within the spawn range, keeping the Y (vertical) axis consistent
            Vector3 spawnPosition = new Vector3(
                Random.Range(-spawnRange, spawnRange),
                0,  // Keep the vertical axis at 0 (ground level)
                Random.Range(-spawnRange, spawnRange)
            );

            // Generate a random Y-axis rotation for variation
            Quaternion randomRotation = Quaternion.Euler(0, Random.Range(0f, 360f), 0);

            // Instantiate a copy of the tree prefab at the random position and rotation
            GameObject treeInstance = Instantiate(treePrefab, spawnPosition, randomRotation);

            // Set a uniform scale for the tree instance
            treeInstance.transform.localScale = treeScale;
        }
    }
}
