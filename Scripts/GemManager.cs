using UnityEngine;

public class Gem : MonoBehaviour
{
    private GemManager gemManager;

    void Start()
    {
        gemManager = FindObjectOfType<GemManager>();
    }

    void OnMouseOver()
    {
        // If the player right-clicks, collect the gem
        if (Input.GetMouseButtonDown(1))
        {
            gemManager.CollectGem();
            Destroy(gameObject);  // Remove the gem from the scene
        }
    }
}
