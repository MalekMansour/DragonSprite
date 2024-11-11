using UnityEngine;

public class Gem : MonoBehaviour
{
    private GemManager gemManager;

    void Start()
    {
        gemManager = FindObjectOfType<GemManager>();
    }

    void OnMouseDown()
    {
        // Collect the gem on left-click
        gemManager.CollectGem();
        Destroy(gameObject);  // Remove the gem from the scene
    }
}
