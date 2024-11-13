using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Respawn : MonoBehaviour
{
    public TextMeshProUGUI healthText; // Reference to the health display
    private int playerHealth = 100;    // Initial player health

    void Start()
    {
        UpdateHealthDisplay();
    }

    // This method should be linked to the restart button in the Inspector
    public void RestartGame()
    {
        // Reset the player's health
        playerHealth = 100;
        UpdateHealthDisplay();

        // Reload the current scene to reset the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Update the health text display (assuming you have a health display as text)
    public void UpdateHealthDisplay()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + playerHealth;
        }
    }

    // Optional method to take damage (if needed for testing)
    public void TakeDamage(int damage)
    {
        playerHealth -= damage;
        if (playerHealth <= 0)
        {
            playerHealth = 0;
            // Optionally, call game over UI here
        }
        UpdateHealthDisplay();
    }
}
