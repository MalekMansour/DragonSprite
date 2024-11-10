using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    public TMP_Text healthText;          // TextMeshPro text to display health
    public GameObject gameOverScreen;    // UI panel for game over screen

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthText();
        gameOverScreen.SetActive(false);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
            currentHealth = 0;

        UpdateHealthText();

        if (currentHealth <= 0)
        {
            GameOver();
        }
    }

    void UpdateHealthText()
    {
        healthText.text = "Health: " + Mathf.RoundToInt(currentHealth); // Display health as integer
    }

    void GameOver()
    {
        gameOverScreen.SetActive(true);
        Time.timeScale = 0f;  // Pause the game
    }

    public void Respawn()
    {
        currentHealth = maxHealth;
        UpdateHealthText();
        gameOverScreen.SetActive(false);
        Time.timeScale = 1f;  // Resume the game

        // Respawn the player at a starting position
        transform.position = Vector3.zero;
    }
}
