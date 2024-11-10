using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerHealth playerHealth;

    public void RestartGame()
    {
        playerHealth.Respawn();
    }
}
