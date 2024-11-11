using UnityEngine;

public class Gem : MonoBehaviour
{
    private GemManager gemManager;

    void Start()
    {
        gemManager = FindObjectOfType<GemManager>();
    }

    void Update()
    {
        // Prevent any unintended rotation
        transform.rotation = Quaternion.identity;
    }

    void OnMouseOver()
    {
        // Collect the gem on left-click
        if (Input.GetMouseButtonDown(0))
        {
            gemManager.CollectGem();
            Destroy(gameObject);  // Remove the gem from the scene
        }
    }
}
