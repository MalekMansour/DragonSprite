using UnityEngine;

public class Gem : MonoBehaviour
{
    private GemManager gemManager;

    void Start()
    {
        // Find and assign the GemManager in the scene
        gemManager = FindObjectOfType<GemManager>();
    }

    void Update()
    {
        // Check for right-click and raycast to see if the player is targeting this gem
        if (Input.GetMouseButtonDown(1))  // Right-click
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit) && hit.transform == transform)
            {
                Collect();
            }
        }
    }

    void Collect()
    {
        // Notify the GemManager and destroy this gem
        gemManager.CollectGem();
        Destroy(gameObject);
    }
}
