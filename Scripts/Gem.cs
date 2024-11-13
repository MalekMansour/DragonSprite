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
        if (gemManager != null)
        {
            gemManager.CollectGem();
            Destroy(gameObject);  // Remove gem from the scene after collection
        }
    }
}
