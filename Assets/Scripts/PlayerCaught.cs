using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    public GameObject gameOverPanel; // Assign this in the Inspector

    private void Start()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false); // Hide the panel at start
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure your player is tagged correctly
        {
            Debug.Log("Player detected! Game Over.");

            // Show the panel
            if (gameOverPanel != null)
                gameOverPanel.SetActive(true);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            // Stop the game
            Time.timeScale = 0f;
        }
    }
}
