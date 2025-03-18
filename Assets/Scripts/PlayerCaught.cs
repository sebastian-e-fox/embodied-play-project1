using UnityEngine;
using UnityEngine.UI; // For UI elements
using UnityEngine.SceneManagement; // If you want to restart the game

public class PlayerDetection : MonoBehaviour
{
    public GameObject gameOverPanel; // Assign this in the Inspector

    private void Start()
    {
        // Make sure the panel is hidden at the start
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Make sure your player has the "Player" tag
        {
            Debug.Log("Player detected! Game Over.");

            // Show the panel
            if (gameOverPanel != null)
                gameOverPanel.SetActive(true);

            // Stop the game
            Time.timeScale = 0f;
        }
    }
}
